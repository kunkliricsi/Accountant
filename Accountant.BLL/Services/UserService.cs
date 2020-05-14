using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AccountantContext _context;

        public UserService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Username or password cannot be null or empty.");

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == username)
                ?? throw new EntityNotFoundException($"User \"{username}\" not found.");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationException();

            return user;
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (_context.Users.Any(u => u.Name == user.Name)) throw new EntityAlreadyExistsException($"Username \"{user.Name}\" is already taken.");

            UpdateUserPassword(ref user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task DeleteUserAsync(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
                return Task.CompletedTask;

            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }

        public Task<User> GetUserAsync(int userId)
        {
            return _context.Users
                .Include(u => u.UserGroups)
                    .ThenInclude(ug => ug.Group)
                .SingleOrDefaultAsync(u => u.Id == userId)
                ?? throw new EntityNotFoundException($"Cannot find user with user ID: {userId}");
        }

        public Task<List<User>> GetUsersAsync(params int[] groupIds)
        {
            return _context.Groups
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .Where(g => groupIds.Contains(g.Id))
                .SelectMany(g => g.UserGroups)
                .Select(ug => ug.User)
                .Distinct()
                .ToListAsync();
        }

        public Task UpdateUserAsync(User user, string password = null)
        {
            var updatedUser = _context.Users.Find(user.Id)
                ?? throw new EntityNotFoundException($"Cannot find user with user ID: {user.Id}");

            if (!string.IsNullOrWhiteSpace(user.Name) && user.Name != updatedUser.Name)
            {
                if (_context.Users.Any(u => u.Name == user.Name))
                    throw new EntityAlreadyExistsException($"User already exists with name: {user.Name}");

                updatedUser.Name = user.Name;
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && user.Email != updatedUser.Email)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                    throw new EntityAlreadyExistsException($"Email address is already taken: {user.Email}");

                updatedUser.Email = user.Email;
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                UpdateUserPassword(ref updatedUser, password);
            }

            _context.Users.Update(updatedUser);
            return _context.SaveChangesAsync();

        }

        private static void UpdateUserPassword(ref User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }

            return true;
        }
    }
}

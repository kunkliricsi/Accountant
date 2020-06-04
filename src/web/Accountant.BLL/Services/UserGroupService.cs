using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly AccountantContext _context;

        public UserGroupService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<(User user, Group group)> CreateUserGroupAsync(int userId, int groupId)
        {
            var (user, group) = await GetUserAndGroupAsync(userId, groupId);

            if (!user.UserGroups.Any(ug => ug.User == user && ug.Group == group))
            {
                var ug = new UserGroup
                {
                    User = user,
                    Group = group,
                };

                user.UserGroups.Add(ug);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return (user, group);
        }

        public async Task DeleteUserGroupAsync(int userId, int groupId)
        {
            var (user, group) = await GetUserAndGroupAsync(userId, groupId);

            var userGroups = user.UserGroups.Where(ug => ug.Group == group).ToList();

            if (userGroups.Any())
            {
                foreach (var ug in userGroups)
                {
                    user.UserGroups.Remove(ug);
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<(User user, Group group)> GetUserAndGroupAsync(int userId, int groupId)
        {
            var user = await _context.Users
                .Include(u => u.UserGroups)
                    .ThenInclude(ug => ug.Group)
                .SingleOrDefaultAsync(u => u.Id == userId)
                ?? throw new EntityNotFoundException($"Cannot find user with ID: {userId}.");

            var group = await _context.Groups.SingleOrDefaultAsync(g => g.Id == groupId)
                ?? throw new EntityNotFoundException($"Cannot find group with ID: {groupId}.");

            return (user, group);
        }
    }
}

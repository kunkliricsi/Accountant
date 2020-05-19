using Accountant.APP.Models;
using Accountant.APP.Models.Helpers;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class UserService : IUserService
    {
        private readonly UsersClient _client;

        public UserService(UsersClient client)
        {
            _client = client;
        }

        public Task AuthenticateUserAsync(string username, string password)
        {
            return _client.LoginAsync(new LoginModel { Name = username, Password = password });
        }

        public Task CreateUserAsync(UpdateModel user)
        {
            return _client.RegisterAsync(user);
        }

        public Task DeleteUserAsync(int userId)
        {
            return _client.DeleteAsync(userId);
        }

        public Task<User> GetUserAsync(int userId)
        {
            return _client.GetAsync(userId);
        }

        public Task<ICollection<User>> GetUsersAsync(params int[] groupIds)
        {
            return _client.GetAllAsync(groupIds);
        }

        public Task UpdateUserAsync(UpdateModel user)
        {
            return _client.PutAsync(user);
        }
    }
}

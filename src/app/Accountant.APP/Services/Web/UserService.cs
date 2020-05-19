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
        private readonly IServiceClientFactory<IUsersClient> _clientFactory;

        public UserService(IServiceClientFactory<IUsersClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task AuthenticateUserAsync(string username, string password)
        {
            return _clientFactory.CreateClient().LoginAsync(new LoginModel { Name = username, Password = password });
        }

        public Task CreateUserAsync(UpdateModel user)
        {
            return _clientFactory.CreateClient().RegisterAsync(user);
        }

        public Task DeleteUserAsync(int userId)
        {
            return _clientFactory.CreateClient().DeleteAsync(userId);
        }

        public Task<User> GetUserAsync(int userId)
        {
            return _clientFactory.CreateClient().GetAsync(userId);
        }

        public Task<ICollection<User>> GetUsersAsync(params int[] groupIds)
        {
            return _clientFactory.CreateClient().GetAllAsync(groupIds);
        }

        public Task UpdateUserAsync(UpdateModel user)
        {
            return _clientFactory.CreateClient().PutAsync(user);
        }
    }
}

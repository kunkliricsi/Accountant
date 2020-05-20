using Accountant.APP.Extensions;
using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public async Task<UserToken> AuthenticateUserAsync(string username, string password)
        {
            var response = await _clientFactory.CreateClient().LoginAsync(new LoginModel { Name = username, Password = password });
            //var result = await response.Stream.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserToken>(response.ToString());
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
            return _clientFactory.CreateClient().GetUserAsync(userId);
        }

        public Task<ICollection<User>> GetUsersAsync(params int[] groupIds)
        {
            return _clientFactory.CreateClient().GetAllUsersAsync(groupIds);
        }

        public Task UpdateUserAsync(UpdateModel user)
        {
            return _clientFactory.CreateClient().PutAsync(user);
        }
    }
}

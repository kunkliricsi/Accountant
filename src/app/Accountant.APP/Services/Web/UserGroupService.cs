using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IServiceClientFactory<IUserGroupClient> _clientFactory;

        public UserGroupService(IServiceClientFactory<IUserGroupClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<User> CreateUserGroupAsync(int userId, int groupId)
        {
            return _clientFactory.CreateClient().PostUserGroupAsync(new UserGroup { UserId = userId, GroupId = groupId });
        }

        public Task DeleteUserGroupAsync(int userId, int groupId)
        {
            return _clientFactory.CreateClient().DeleteUserGroupAsync(new UserGroup { UserId = userId, GroupId = groupId });
        }
    }
}

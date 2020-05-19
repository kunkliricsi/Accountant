using Accountant.APP.Models;
using Accountant.APP.Models.Helpers;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class UserGroupService : IUserGroupService
    {
        private readonly UserGroupClient _client;

        public UserGroupService(UserGroupClient client)
        {
            _client = client;
        }

        public Task<User> CreateUserGroupAsync(int userId, int groupId)
        {
            return _client.PostUserGroupAsync(new UserGroup { UserId = userId, GroupId = groupId });
        }

        public Task DeleteUserGroupAsync(int userId, int groupId)
        {
            return _client.DeleteUserGroupAsync(new UserGroup { UserId = userId, GroupId = groupId });
        }
    }
}

using Accountant.APP.Models;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IUserGroupService
    {
        Task<User> CreateUserGroupAsync(int userId, int groupId);

        Task DeleteUserGroupAsync(int userId, int groupId);
    }
}

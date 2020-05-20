using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IUserService
    {
        Task<UserToken> AuthenticateUserAsync(string username, string password);
        Task<User> GetUserAsync(int userId);
        Task<ICollection<User>> GetUsersAsync(params int[] groupIds);
        Task CreateUserAsync(UpdateModel user);
        Task UpdateUserAsync(UpdateModel user);
        Task DeleteUserAsync(int userId);
    }
}

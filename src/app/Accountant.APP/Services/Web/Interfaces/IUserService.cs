using Accountant.APP.Models;
using Accountant.APP.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IUserService
    {
        Task AuthenticateUserAsync(string username, string password);
        Task<User> GetUserAsync(int userId);
        Task<ICollection<User>> GetUsersAsync(params int[] groupIds);
        Task CreateUserAsync(UpdateModel user);
        Task UpdateUserAsync(UpdateModel user);
        Task DeleteUserAsync(int userId);
    }
}

using Accountant.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<User> GetUserAsync(int userId);
        Task<List<User>> GetUsersAsync(params int[] groupIds);
        Task<User> CreateUserAsync(User user, string password);
        Task UpdateUserAsync(User user, string password = null);
        Task DeleteUserAsync(int userId);
    }
}

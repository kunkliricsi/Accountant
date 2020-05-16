using Accountant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.BLL.Interfaces
{
    public interface IUserGroupService
    {
        Task<User> CreateUserGroupAsync(int userId, int groupId);

        Task DeleteUserGroupAsync(int userId, int groupId);
    }
}

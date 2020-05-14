using Accountant.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.BLL.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetGroupsAsync(params int[] userIds);
        Task<Group> GetGroupAsync(int groupId);
        Task<Group> CreateGroupAsync(Group group);
        Task UpdateGroupAsync(Group group);
        Task DeleteGroupAsync(int groupId);
    }
}

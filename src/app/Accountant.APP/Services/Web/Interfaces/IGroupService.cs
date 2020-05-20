using Accountant.APP.Models.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IGroupService
    {
        Task<ICollection<Group>> GetGroupsAsync(params int[] userIds);
        Task<Group> GetGroupAsync(int groupId);
        Task<Group> CreateGroupAsync(Group group);
        Task UpdateGroupAsync(Group group);
        Task DeleteGroupAsync(int groupId);
    }
}

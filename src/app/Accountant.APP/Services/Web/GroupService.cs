using Accountant.APP.Models;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class GroupService : IGroupService
    {
        private readonly GroupsClient _client;

        public GroupService(GroupsClient client)
        {
            _client = client;
        }

        public Task<Group> CreateGroupAsync(Group group)
        {
            return _client.PostAsync(group);
        }

        public Task DeleteGroupAsync(int groupId)
        {
            return _client.DeleteAsync(groupId);
        }

        public Task<Group> GetGroupAsync(int groupId)
        {
            return _client.GetAsync(groupId);
        }

        public Task<ICollection<Group>> GetGroupsAsync(params int[] userIds)
        {
            return _client.GetAllAsync(userIds);
        }

        public Task UpdateGroupAsync(Group group)
        {
            return _client.PutAsync(group);
        }
    }
}

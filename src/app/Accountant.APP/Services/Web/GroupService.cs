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
        private readonly IServiceClientFactory<IGroupsClient> _clientFactory;

        public GroupService(IServiceClientFactory<IGroupsClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<Group> CreateGroupAsync(Group group)
        {
            return _clientFactory.CreateClient().PostAsync(group);
        }

        public Task DeleteGroupAsync(int groupId)
        {
            return _clientFactory.CreateClient().DeleteAsync(groupId);
        }

        public Task<Group> GetGroupAsync(int groupId)
        {
            return _clientFactory.CreateClient().GetAsync(groupId);
        }

        public Task<ICollection<Group>> GetGroupsAsync(params int[] userIds)
        {
            return _clientFactory.CreateClient().GetAllAsync(userIds);
        }

        public Task UpdateGroupAsync(Group group)
        {
            return _clientFactory.CreateClient().PutAsync(group);
        }
    }
}

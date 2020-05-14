using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly AccountantContext _context;

        public GroupService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return group;
        }

        public Task DeleteGroupAsync(int groupId)
        {
            var group = _context.Groups.Find(groupId);

            if (group == null)
                return Task.CompletedTask;

            _context.Groups.Remove(group);
            return _context.SaveChangesAsync();
        }

        public Task<Group> GetGroupAsync(int groupId)
        {
            return _context.Groups
                .Include(g => g.Reports)
                .Include(g => g.ShoppingLists)
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .SingleOrDefaultAsync(g => g.Id == groupId)
                ?? throw new EntityNotFoundException($"Cannot find group with ID: {groupId}");
        }

        public Task<List<Group>> GetGroupsAsync(params int[] userIds)
        {
            return _context.Users
                .Include(u => u.UserGroups)
                    .ThenInclude(ug => ug.Group)
                .Where(u => userIds.Contains(u.Id))
                .SelectMany(u => u.UserGroups)
                .Select(ug => ug.Group)
                .Distinct()
                .ToListAsync();
        }

        public Task UpdateGroupAsync(Group group)
        {
            var updatedGroup = _context.Groups.Find(group.Id)
                ?? throw new EntityNotFoundException($"Cannot find group with ID: {group.Id}");

            if (!string.IsNullOrWhiteSpace(group.Name))
            {
                updatedGroup.Name = group.Name;
            }

            _context.Groups.Update(updatedGroup);
            return _context.SaveChangesAsync();
        }
    }
}

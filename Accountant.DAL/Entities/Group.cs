using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserGroup> UserGroups { get; } = new List<UserGroup>();

        public ICollection<Report> Reports { get; } = new List<Report>();

        public ICollection<ShoppingList> ShoppingLists { get; } = new List<ShoppingList>();
    }
}

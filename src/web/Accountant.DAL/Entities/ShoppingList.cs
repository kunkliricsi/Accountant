using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.DAL.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<ShoppingListItem> ShoppingListItems { get; } = new List<ShoppingListItem>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.DAL.Entities
{
    public class ShoppingListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool? IsTicked { get; set; }

        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}

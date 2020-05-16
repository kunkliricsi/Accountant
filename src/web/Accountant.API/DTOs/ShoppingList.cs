using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Shopping List name is required.")]
        public string Name { get; set; }

        public ICollection<ShoppingListItem> Items { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs
{
    public class ShoppingListItem
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Shopping list item name is required.")]
        public string Name { get; set; }

        public bool? IsTicked { get; set; }

        public ShoppingList ShoppingList { get; set; }
    }
}

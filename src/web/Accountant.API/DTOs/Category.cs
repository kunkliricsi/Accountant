using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs
{
    public class Category
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

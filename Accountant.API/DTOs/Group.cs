using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs
{
    public class Group
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Group name is required.")]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Report> Reports { get; set; }

        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}

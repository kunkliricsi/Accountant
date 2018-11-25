using System;
using System.ComponentModel.DataAnnotations;

namespace Accountant.Models
{
    public class ShoppingListItem
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }

        public int ExpenseID { get; set; }

        public DateTime lastModified { get; set; }
    }
}
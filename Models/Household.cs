using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accountant.Models
{
    public class Household
    {
        public int ID { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<ShoppingListItem> ShoppingList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Accountant.Models.Enums;

namespace Accountant.Models
{
    public class Expense
    {
        public int ID { get; set; }

        [Required]
        public virtual User Purchaser { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ShoppingListItem> ItemsPurchased { get; set; }
        public PayOption? PayOption { get; set; }
        public DateTime DateOfPurchase { get; set; }

        [Required]
        public virtual Report Report { get; set; }
    }
}
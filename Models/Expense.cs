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
        public User Purchaser { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }
        public Category Category { get; set; }
        public ICollection<ShoppingListItem> ItemsPurchased { get; set; }
        public PayOption? PayOption { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public int ReportID { get; set; }
        public Report Report { get; set; }
    }
}
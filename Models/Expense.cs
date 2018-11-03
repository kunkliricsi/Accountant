using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Accountant.Models.Enums;

namespace Accountant.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public User Purchaser { get; set; }
        
        [DataType(DataType.Currency)]
        public int Amount { get; set; }
        public Category Category { get; set; }
        public ICollection<ShoppingListItem> ItemsPurchased { get; set; }
        public PayOption PayOption { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
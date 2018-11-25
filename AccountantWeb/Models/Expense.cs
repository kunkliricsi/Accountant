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
        [DataType(DataType.Currency)]
        public int Amount { get; set; }
        public PayOption? PayOption { get; set; }
        public DateTime DateOfPurchase { get; set; }

        [Required]
        public int ReportID { get; set; }
        public int CategoryID { get; set; }

        [Required]
        public int PurchaserID { get; set; }

        public DateTime lastModified { get; set; }
    }
}
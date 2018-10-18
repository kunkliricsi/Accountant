using System;
using Accountant.Models.Enums;

namespace Accountant.Models
{
    public class Expenses
    {
        public Guid ID { get; set; }
        public int Amount { get; set; }
        public Categories Category { get; set; }
        public PayOptions PayOption { get; set; }
    }
}
using System;

namespace Accountant.Models
{
    public class Changes
    {
        public int ID { get; set; }
        public DateTime Category { get; set; }
        public DateTime Expense { get; set; }
        public DateTime Report { get; set; }
        public DateTime ShoppingListItem { get; set; }
        public DateTime User { get; set; }
    }
}
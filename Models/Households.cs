using System;

namespace Accountant.Models
{
    public class Households
    {
        public Guid ID { get; set; }
        public Users User { get; set; }
        public Expenses Expense { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
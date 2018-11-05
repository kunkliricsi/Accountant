using System;
using System.Collections.Generic;

namespace Accountant.Models
{
    public class Report
    {
        public int ID { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public bool Evaluated { get; set; }
        public DateTime DateOfEvaluation { get; set; }
    }
}
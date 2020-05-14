using System;
using System.Collections.Generic;

namespace Accountant.DAL.Entities
{
    public class Report
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsEvaluated { get; set; }
        public DateTime? EvaluationDate { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Expense> Expenses { get; } = new List<Expense>();
    }
}

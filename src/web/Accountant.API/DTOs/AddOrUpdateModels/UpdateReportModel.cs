using System;
using System.Collections.Generic;

namespace Accountant.API.DTOs.AddOrUpdateModels
{
    public class UpdateReportModel
    {
        public int Id { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsEvaluated { get; set; }
        public DateTime? EvaluationDate { get; set; }

        public int? GroupId { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}

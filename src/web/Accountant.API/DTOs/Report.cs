using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs
{
    public class Report
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Report start date is required.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Report end date is required.")]
        public DateTime? EndDate { get; set; }

        public bool IsEvaluated { get; set; }
        public DateTime? EvaluationDate { get; set; }

        [Required(ErrorMessage = "Report owner group is required.")]
        public Group Group { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}

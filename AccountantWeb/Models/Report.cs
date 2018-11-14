using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Accountant.Models
{
    public class Report
    {
        public int ID { get; set; }
        public IQueryable<Expense> Expenses { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public bool Evaluated { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfEvaluation { get; set; }
    }
}
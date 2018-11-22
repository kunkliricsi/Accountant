using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accountant.Models
{
    public class Report
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public bool Evaluated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfEvaluation { get; set; }
    }
}
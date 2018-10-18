using System;

namespace Accountant.Models
{
    public class Reports
    {
        public Guid ID { get; set; }
        public Households HouseHold { get; set; }
        public bool Evaluated { get; set; }
        public DateTime EvaluationTime { get; set; }
    }
}
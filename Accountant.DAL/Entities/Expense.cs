using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Accountant.DAL.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int ReportId { get; set; }
        public Report Report { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

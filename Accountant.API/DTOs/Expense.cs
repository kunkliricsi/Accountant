using System;
using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs
{
    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be higher than 0.")]
        public int? Amount { get; set; }

        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime? PurchaseDate { get; set; }

        [Required(ErrorMessage = "Report is required for an expense.")]
        public Report Report { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "User is required.")]
        public User User { get; set; }
    }
}

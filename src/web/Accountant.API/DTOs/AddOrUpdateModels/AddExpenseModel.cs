using System;
using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs.AddOrUpdateModels
{
    public class AddExpenseModel
    {
        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be higher than 0.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Report is required for an expense.")]
        public int ReportId { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }
    }
}

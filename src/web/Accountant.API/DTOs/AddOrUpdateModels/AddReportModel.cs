using System;
using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs.AddOrUpdateModels
{
    public class AddReportModel
    {
        [Required(ErrorMessage = "Report start date is required.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Report end date is required.")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Report owner group is required.")]
        public int GroupId { get; set; }
    }
}

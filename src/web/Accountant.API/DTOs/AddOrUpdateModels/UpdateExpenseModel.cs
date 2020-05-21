using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.API.DTOs.AddOrUpdateModels
{
    public class UpdateExpenseModel
    {
        public int Id { get; set; }

        public int? Amount { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public int? ReportId { get; set; }

        public int? CategoryId { get; set; }

        public int? UserId { get; set; }
    }
}

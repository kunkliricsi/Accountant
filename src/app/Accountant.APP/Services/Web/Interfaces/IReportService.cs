﻿using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IReportService
    {
        Task<ICollection<Report>> GetReportsAsync(int groupId);
        Task<Report> GetCurrentReportAsync(int groupId);
        Task<Report> GetReportAsync(int reportId);
        Task<Report> CreateReportAsync(AddReportModel report);
        Task UpdateReportAsync(UpdateReportModel report);
        Task EvaluateReportAsync(int reportId, DateTime evaluationDate);
        Task DeleteReportAsync(int reportId);
    }
}

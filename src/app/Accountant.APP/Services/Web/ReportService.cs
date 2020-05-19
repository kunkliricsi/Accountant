using Accountant.APP.Models;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class ReportService : IReportService
    {
        private readonly ReportsClient _client;

        public ReportService(ReportsClient client)
        {
            _client = client;
        }

        public Task<Report> CreateReportAsync(Report report)
        {
            return _client.PostCurrentAsync(report);
        }

        public Task DeleteReportAsync(int reportId)
        {
            return _client.DeleteAsync(reportId);
        }

        public Task EvaluateReportAsync(int reportId, DateTime evaluationDate)
        {
            return _client.Put2Async(reportId, evaluationDate);
        }

        public Task<Report> GetCurrentReportAsync(int groupId)
        {
            return _client.GetCurrentAsync(groupId);
        }

        public Task<Report> GetReportAsync(int reportId)
        {
            return _client.GetAsync(reportId);
        }

        public Task<ICollection<Report>> GetReportsAsync(int groupId)
        {
            return _client.GetAllAsync(groupId);
        }

        public Task UpdateReportAsync(Report report)
        {
            return _client.PutAsync(report);
        }
    }
}

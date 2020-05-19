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
        private readonly IServiceClientFactory<IReportsClient> _clientFactory;

        public ReportService(IServiceClientFactory<IReportsClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<Report> CreateReportAsync(Report report)
        {
            return _clientFactory.CreateClient().PostCurrentAsync(report);
        }

        public Task DeleteReportAsync(int reportId)
        {
            return _clientFactory.CreateClient().DeleteAsync(reportId);
        }

        public Task EvaluateReportAsync(int reportId, DateTime evaluationDate)
        {
            return _clientFactory.CreateClient().Put2Async(reportId, evaluationDate);
        }

        public Task<Report> GetCurrentReportAsync(int groupId)
        {
            return _clientFactory.CreateClient().GetCurrentAsync(groupId);
        }

        public Task<Report> GetReportAsync(int reportId)
        {
            return _clientFactory.CreateClient().GetAsync(reportId);
        }

        public Task<ICollection<Report>> GetReportsAsync(int groupId)
        {
            return _clientFactory.CreateClient().GetAllAsync(groupId);
        }

        public Task UpdateReportAsync(Report report)
        {
            return _clientFactory.CreateClient().PutAsync(report);
        }
    }
}

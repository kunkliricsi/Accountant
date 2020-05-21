using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
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

        public Task<Report> CreateReportAsync(AddReportModel report)
        {
            return _clientFactory.CreateClient().PostAsync(report);
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
            return _clientFactory.CreateClient().GetCurrentReportAsync(groupId);
        }

        public Task<Report> GetReportAsync(int reportId)
        {
            return _clientFactory.CreateClient().GetReportAsync(reportId);
        }

        public Task<ICollection<Report>> GetReportsAsync(int groupId)
        {
            return _clientFactory.CreateClient().GetAllReportsAsync(groupId);
        }

        public Task UpdateReportAsync(UpdateReportModel report)
        {
            return _clientFactory.CreateClient().PutAsync(report);
        }
    }
}

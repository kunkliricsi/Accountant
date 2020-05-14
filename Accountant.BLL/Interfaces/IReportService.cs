using Accountant.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.BLL.Interfaces
{
    public interface IReportService
    {
        Task<List<Report>> GetReportsAsync(int groupId);
        Task<Report> GetCurrentReportAsync(int groupId);
        Task<Report> GetReportAsync(int reportId);
        Task<Report> CreateReportAsync(Report report);
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(int reportId);
    }
}

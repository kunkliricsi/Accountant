using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly AccountantContext _context;

        public ReportService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<Report> CreateReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public Task DeleteReportAsync(int reportId)
        {
            var report = _context.Reports.Find(reportId);

            if (report == null)
                return Task.CompletedTask;

            _context.Reports.Remove(report);
            return _context.SaveChangesAsync();
        }

        public Task<Report> GetCurrentReportAsync(int groupId)
        {
            return _context.Reports
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.User)
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.Category)
                .OrderByDescending(r => r.EndDate)
                .FirstOrDefaultAsync(r => r.GroupId == groupId)
                ?? throw new EntityNotFoundException($"Cannot find group with ID: {groupId}");
        }

        public Task<Report> GetReportAsync(int reportId)
        {
            return _context.Reports.SingleOrDefaultAsync(r => r.Id == reportId)
                ?? throw new EntityNotFoundException($"Cannot find report with ID: {reportId}");
        }

        public Task<List<Report>> GetReportsAsync(int groupId)
        {
            return _context.Reports
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.User)
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.Category)
                .Where(r => r.GroupId == groupId)
                .ToListAsync();
        }

        public Task UpdateReportAsync(Report report)
        {
            var updatedReport = _context.Reports.Find(report.Id)
                ?? throw new EntityNotFoundException($"Cannot find report with ID: {report.Id}");

            if (report.StartDate != default)
            {
                updatedReport.StartDate = report.StartDate;
            }

            if (report.EndDate != default)
            {
                updatedReport.EndDate = report.EndDate;
            }

            if (report.IsEvaluated && !updatedReport.IsEvaluated)
            {
                updatedReport.IsEvaluated = true;
            }

            if (report.EvaluationDate != default)
            {
                updatedReport.EvaluationDate = report.EvaluationDate;
            }

            _context.Reports.Update(updatedReport);
            return _context.SaveChangesAsync();
        }
    }
}

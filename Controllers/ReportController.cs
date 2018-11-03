using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AccountantContext context;

        public ReportController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public ActionResult<ICollection<Report>> GetAll()
        {
            return context.Reports.ToList();
        }

        [HttpGet("{id}", Name = "GetReport")]
        public ActionResult<Report> GetById(int id)
        {
            var report = context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPost]
        public IActionResult Create(Report report)
        {
            context.Reports.Add(report);
            context.SaveChanges();

            return CreatedAtAction("GetReport", new { id = report.ID }, report);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Report report)
        {
            var reportToUpdate = context.Reports.Find(id);
            if (reportToUpdate == null)
            {
                return NotFound();
            }

            reportToUpdate.Expenses = report.Expenses;
            reportToUpdate.Evaluated = report.Evaluated;
            reportToUpdate.DateOfEvaluation = report.DateOfEvaluation;

            context.Reports.Update(reportToUpdate);
            context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}/expense")]
        public ActionResult<ICollection<Expense>> GetAllExpenses(int id)
        {
            var expenses = context.Reports.Find(id)?.Expenses.ToList();
            if (expenses == null)
            {
                return NotFound();
            }

            return expenses;
        }

        [HttpGet("{id}/expense/{ReportId}", Name = "GetExpense")]
        public ActionResult<Expense> GetExpenseById(int id, int ReportId)
        {
            var report = context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            var expenseToReturn = report.Expenses.FirstOrDefault(e => e.ID == ReportId);
            if (expenseToReturn == null)
            {
                return NotFound();
            }

            return expenseToReturn;
        }

        [HttpPost("{id}/expense")]
        public IActionResult CreateExpense(int id, Expense expense)
        {
            var report = context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            report.Expenses.Add(expense);
            context.Expenses.Add(expense);
            context.Reports.Update(report);
            context.SaveChanges();

            return CreatedAtAction("GetExpense", new { id = expense.ID}, expense);
        }
    }
}
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

        [HttpGet("{id}")]
        public ActionResult<Report> GetById(int id)
        {
            var report = context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            return report;
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
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AccountantContext context;

        public ReportController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public ActionResult<ICollection<Report>> Get()
        {
            return context.Reports.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Report> Get(int id)
        {
            var report = context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpPost]
        public IActionResult Post(Report report)
        {
            context.Reports.Add(report);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = report.ID }, report);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Report report)
        {
            var reportToUpdate = context.Reports.Find(id);
            if (reportToUpdate == null)
            {
                return NotFound();
            }

            reportToUpdate.Start = report.Start;
            reportToUpdate.End = report.End;
            reportToUpdate.Evaluated = report.Evaluated;
            reportToUpdate.DateOfEvaluation = report.DateOfEvaluation;

            context.Reports.Update(reportToUpdate);
            context.SaveChanges();
            return NoContent();
        }
    }
}
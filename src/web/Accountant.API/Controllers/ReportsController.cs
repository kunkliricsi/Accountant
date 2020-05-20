using Accountant.API.DTOs;
using Accountant.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportService service, IMapper mapper, ILogger<ReportsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{groupId}/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Report>>> GetAllReportsAsync(int groupId)
        {
            _logger.LogInformation($"Getting all reports of group with ID: {groupId}...");

            return _mapper.Map<List<Report>>(await _service.GetReportsAsync(groupId));
        }

        [HttpGet("{groupId}/current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Report>> GetCurrentReportAsync(int groupId)
        {
            _logger.LogInformation($"Getting current report of group with ID: {groupId}...");

            return _mapper.Map<Report>(await _service.GetCurrentReportAsync(groupId));
        }

        [HttpGet("{id}", Name = "GetReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Report>> GetReportAsync(int id)
        {
            _logger.LogInformation($"Getting report with ID: {id}...");

            return _mapper.Map<Report>(await _service.GetReportAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Report>> PostCurrentAsync(Report report)
        {
            _logger.LogInformation("Creating report...");

            var created = await _service.CreateReportAsync(
                _mapper.Map<DAL.Entities.Report>(report));

            _logger.LogInformation($"Created report [{created.Id}].");

            return CreatedAtAction(
                "GetReport",
                new { id = created.Id },
                _mapper.Map<Report>(created));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync(Report report)
        {
            _logger.LogInformation($"Updating report [{report.Id}]...");

            await _service.UpdateReportAsync(
                _mapper.Map<DAL.Entities.Report>(report));

            return NoContent();
        }

        [HttpPut("{id}/evaluate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] DateTime evaluationDate)
        {
            _logger.LogInformation($"Evaluating report [{id}]...");

            await _service.EvaluateReportAsync(id, evaluationDate);

            _logger.LogInformation($"Report [{id}] evaluated: {evaluationDate}.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting report [{id}]...");

            await _service.DeleteReportAsync(id);

            return NoContent();
        }
    }
}

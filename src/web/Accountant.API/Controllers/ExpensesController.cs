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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(IExpenseService service, IMapper mapper, ILogger<ExpensesController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Expense>>> GetAllExpensesAsync([FromQuery(Name = "reportId")] int[] reportIds)
        {
            _logger.LogInformation($"Getting all expenses of report(s) with ID(s): [{string.Join(", ", reportIds)}]...");

            return _mapper.Map<List<Expense>>(await _service.GetExpensesAsync(reportIds));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Expense>> PostAsync([FromBody] Expense expense)
        {
            _logger.LogInformation("Creating expense...");

            var created = await _service.CreateExpenseAsync(
                _mapper.Map<DAL.Entities.Expense>(expense));

            _logger.LogInformation($"Created expense [{created.Id}].");

            return CreatedAtAction(
                "GetAllExpenses",
                _mapper.Map<Expense>(created));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync([FromBody] Expense expense)
        {
            _logger.LogInformation($"Updating expense [{expense.Id}]...");

            await _service.UpdateExpenseAsync(
                _mapper.Map<DAL.Entities.Expense>(expense));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting expense [{id}]...");

            await _service.DeleteExpenseAsync(id);

            return NoContent();
        }
    }
}

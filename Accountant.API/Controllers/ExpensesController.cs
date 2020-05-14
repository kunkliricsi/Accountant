﻿using Accountant.API.DTOs;
using Accountant.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Expense>>> GetAllAsync([FromQuery(Name = "id")] string[] ids)
        {
            _logger.LogInformation($"Getting all expenses for report with ID: [{string.Join(", ", ids)}]...");

            var intIds = ids.Select(id => int.Parse(id)).ToArray();

            return _mapper.Map<List<Expense>>(await _service.GetExpensesAsync(intIds));
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostAsync([FromBody] Expense expense)
        {
            _logger.LogInformation("Creating expense...");

            var created = await _service.CreateExpenseAsync(
                _mapper.Map<DAL.Entities.Expense>(expense));

            _logger.LogInformation($"Created expese: [{created.Id}].");

            return CreatedAtAction(
                nameof(GetAllAsync),
                _mapper.Map<Expense>(created));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Expense expense)
        {
            _logger.LogInformation($"Updating expense: [{expense.Id}]...");

            await _service.UpdateExpenseAsync(
                _mapper.Map<DAL.Entities.Expense>(expense));

            return NoContent();
        }

        [HttpDelete("{id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting expense: [{id}]...");

            await _service.DeleteExpenseAsync(id);

            return NoContent();
        }
    }
}

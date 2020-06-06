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
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingListService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ShoppingListsController> _logger;

        public ShoppingListsController(IShoppingListService service, IMapper mapper, ILogger<ShoppingListsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetShoppingList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingList>> GetShoppingListAsync(int id)
        {
            _logger.LogInformation($"Getting shopping list [{id}]...");

            return _mapper.Map<ShoppingList>(await _service.GetShoppingListAsync(id));
        }

        [HttpPost("{groupId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ShoppingList>> PostAsync([FromBody] ShoppingList list, int groupId)
        {
            _logger.LogInformation("Creating shopping list...");

            var created = await _service.CreateShoppingListAsync(
                _mapper.Map<DAL.Entities.ShoppingList>(list));

            _logger.LogInformation($"Created shopping list [{created.Id}].");

            return CreatedAtAction(
                "Get",
                new { id = created.Id },
                _mapper.Map<ShoppingList>(created));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync([FromBody] ShoppingList list)
        {
            _logger.LogInformation($"Updating shopping list [{list.Id}]...");

            await _service.UpdateShoppingListAsync(
                _mapper.Map<DAL.Entities.ShoppingList>(list));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting shopping list [{id}]...");

            await _service.DeleteShoppingListAsync(id);

            return NoContent();
        }

        [HttpPost("items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ShoppingListItem>> PostAsync([FromBody] ShoppingListItem listItem)
        {
            _logger.LogInformation($"Creating shopping list item...");

            var created = await _service.CreateShoppingListItemAsync(
                _mapper.Map<DAL.Entities.ShoppingListItem>(listItem));

            _logger.LogInformation($"Created shopping list item [{created.Id}].");

            return CreatedAtAction(
                "GetShoppingList",
                new { id = created.ShoppingListId },
                _mapper.Map<ShoppingListItem>(created));
        }

        [HttpPut("items")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync([FromBody] ShoppingListItem listItem)
        {
            _logger.LogInformation($"Updating shopping list item [{listItem.Id}]...");

            await _service.UpdateShoppingListItemAsync(
                _mapper.Map<DAL.Entities.ShoppingListItem>(listItem));

            return NoContent();
        }

        [HttpDelete("items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteItemAsync(int itemId)
        {
            _logger.LogInformation($"Deleting shopping list item [{itemId}]...");

            await _service.DeleteShoppingListItemAsync(itemId);

            return NoContent();
        }
    }
}

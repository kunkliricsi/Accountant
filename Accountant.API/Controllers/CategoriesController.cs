using Accountant.API.DTOs;
using Accountant.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService service, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _categoryService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllAsync()
        {
            _logger.LogInformation("Getting all categories...");

            return _mapper.Map<List<Category>>(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostAsync([FromBody] Category category)
        {
            _logger.LogInformation("Creating category...");

            var created = await _categoryService.CreateCategoryAsync(
                _mapper.Map<DAL.Entities.Category>(category));

            _logger.LogInformation($"Created category: [{category.Id}].");

            return CreatedAtAction(
                nameof(GetAllAsync),
                new { id = created.Id },
                _mapper.Map<Category>(created));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Category category)
        {
            _logger.LogInformation($"Updating category: [{category.Id}]...");

            await _categoryService.UpdateCategoryAsync(
                _mapper.Map<DAL.Entities.Category>(category));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting category: [{id}]...");

            await _categoryService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}

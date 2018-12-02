using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
         private readonly AccountantContext context;

        public CategoryController(AccountantContext context)
        {
            this.context = context;  
        }
 
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                NotFound();
            }

            return category;
        }

        [HttpGet]
        public ActionResult<ICollection<Category>> Get()
        {
            return context.Categories.ToList();
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new {id = category.ID }, category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryToDelete = context.Categories.Find(id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }

            context.Categories.Remove(categoryToDelete);
            context.SaveChanges();

            return NoContent();
        }
    }
}
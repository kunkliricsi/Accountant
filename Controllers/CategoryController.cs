using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AccountantContext context;

        public CategoryController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                NotFound();
            }

            return category;
        }

        [HttpGet]
        public ActionResult<ICollection<Category>> GetAll()
        {
            return context.Categories.ToList();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();

            return CreatedAtAction("GetCategory", new {id = category.ID }, category);
        }

        [HttpDelete]
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
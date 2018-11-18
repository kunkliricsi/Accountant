using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly AccountantContext context;

        public ShoppingListController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public ActionResult<ICollection<ShoppingListItem>> Get()
        {
            return context.ShoppingList.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingListItem> Get(int id)
        {
            var item = context.ShoppingList.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Post(ShoppingListItem item)
        {
            context.ShoppingList.Add(item);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ShoppingListItem item)
        {
            var itemToUpdate = context.ShoppingList.Find(id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = item.Name;
            itemToUpdate.DateOfCreation = item.DateOfCreation;
            itemToUpdate.Comment = item.Comment;

            context.ShoppingList.Update(itemToUpdate);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var itemToDelete = context.ShoppingList.Find(id);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            context.ShoppingList.Remove(itemToDelete);
            context.SaveChanges();
            return NoContent();
        }
    }
}
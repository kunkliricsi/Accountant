using System.Collections.Generic;
using Accountant.Models;
using Accountant.Data;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AccountantContext context;

        public UserController(AccountantContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<ICollection<User>> Get()
        {
            return context.Users.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var item = context.Users.Find(id);
            if (item == null) 
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return CreatedAtRoute(nameof(Get), new { id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            var userToUpdate = context.Users.Find(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            context.Users.Update(userToUpdate);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userToDelete = context.Users.Find(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            context.Users.Remove(userToDelete);
            context.SaveChanges();
            return NoContent();
        }
    }
}


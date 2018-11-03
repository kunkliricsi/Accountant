using System.Collections.Generic;
using Accountant.Models;
using Accountant.Data;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AccountantContext context;

        public UserController(AccountantContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<ICollection<User>> GetAll()
        {
            return context.Users.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            var item = context.Users.Find(id);
            if (item == null) 
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
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


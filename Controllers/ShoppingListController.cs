using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly AccountantContext context;

        public ShoppingListController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public ActionResult<ICollection<Report>> GetAll()
        {
            return context.ShoppingList.ToList();
        }
    }
}
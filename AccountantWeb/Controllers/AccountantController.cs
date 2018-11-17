using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Accountant.Data;
using Accountant.Models;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    [Route("")]
    public class AccountantController : ControllerBase
    {
        private readonly AccountantContext context;

        public AccountantController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public ActionResult<Changes> Get()
        {
            var change = context.Changes.Find(1);
            if (change == null)
            {
                NotFound();
            }

            return change;
        }
    }
}
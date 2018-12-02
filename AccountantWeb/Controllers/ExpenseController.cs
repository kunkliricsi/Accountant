using System.Collections.Generic;
using Accountant.Models;
using Accountant.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Accountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly AccountantContext context;

        public ExpenseController(AccountantContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public ActionResult<ICollection<Expense>> Get()
        {
            return context.Expenses.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Expense> Get(int id)
        {
            var expense = context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        [HttpPost]
        public IActionResult Post(Expense expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = expense.ID }, expense);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Expense expense)
        {
            var expenseToUpdate = context.Expenses.Find(id);
            if (expenseToUpdate == null)
            {
                return NotFound();
            }

            expenseToUpdate.Amount = expense.Amount;
            expenseToUpdate.CategoryID = expense.CategoryID;
            expenseToUpdate.DateOfPurchase = expense.DateOfPurchase;
            expenseToUpdate.PayOption = expense.PayOption;
            expenseToUpdate.PurchaserID = expense.PurchaserID;

            context.Expenses.Update(expenseToUpdate);
            context.SaveChanges();

            return NoContent();
        }
    }
}
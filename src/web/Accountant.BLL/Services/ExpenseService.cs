using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AccountantContext _context;

        public ExpenseService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return await _context.Expenses
                .Include(e => e.Category)
                .Include(e => e.User)
                .Include(e => e.Report)
                .SingleOrDefaultAsync(e => e.Id == expense.Id);
        }

        public Task DeleteExpenseAsync(int expenseId)
        {
            var expense = _context.Expenses.Find(expenseId);

            if (expense == null)
                return Task.CompletedTask;

            _context.Expenses.Remove(expense);
            return _context.SaveChangesAsync();
        }

        public Task<List<Expense>> GetExpensesAsync(params int[] reportIds)
        {
            return _context.Reports
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.Category)
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.User)
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.Report)
                .Where(r => reportIds.Contains(r.Id))
                .SelectMany(r => r.Expenses)
                .Distinct()
                .ToListAsync();
        }

        public Task UpdateExpenseAsync(Expense expense)
        {
            var updatedExpense = _context.Expenses.Find(expense.Id)
                ?? throw new EntityNotFoundException($"Cannot find expense with ID: {expense.Id}");

            if (expense.Amount != default)
            {
                updatedExpense.Amount = expense.Amount;
            }

            if (expense.PurchaseDate != default)
            {
                updatedExpense.PurchaseDate = expense.PurchaseDate;
            }

            if (expense.ReportId != default)
            {
                updatedExpense.ReportId = expense.ReportId;
            }

            if (expense.CategoryId != default)
            {
                updatedExpense.CategoryId = expense.CategoryId;
            }

            if (expense.UserId != default)
            {
                updatedExpense.UserId = expense.UserId;
            }

            _context.Expenses.Update(updatedExpense);
            return _context.SaveChangesAsync();
        }
    }
}

using Accountant.APP.Models;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class ExpenseService : IExpenseService
    {
        private readonly ExpensesClient _client;

        public ExpenseService(ExpensesClient client)
        {
            _client = client;
        }

        public Task<Expense> CreateExpenseAsync(Expense expense)
        {
            return _client.PostAsync(expense);
        }

        public Task DeleteExpenseAsync(int expenseId)
        {
            return _client.DeleteAsync(expenseId);
        }

        public Task<ICollection<Expense>> GetExpensesAsync(params int[] reportIds)
        {
            return _client.GetAllAsync(reportIds);
        }

        public Task UpdateExpenseAsync(Expense expense)
        {
            return _client.PutAsync(expense);
        }
    }
}

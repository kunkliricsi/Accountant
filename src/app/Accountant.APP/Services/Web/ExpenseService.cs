using Accountant.APP.Models.Web;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class ExpenseService : IExpenseService
    {
        private readonly IServiceClientFactory<IExpensesClient> _clientFactory;

        public ExpenseService(IServiceClientFactory<IExpensesClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<Expense> CreateExpenseAsync(Expense expense)
        {
            return _clientFactory.CreateClient().PostAsync(expense);
        }

        public Task DeleteExpenseAsync(int expenseId)
        {
            return _clientFactory.CreateClient().DeleteAsync(expenseId);
        }

        public Task<ICollection<Expense>> GetExpensesAsync(params int[] reportIds)
        {
            return _clientFactory.CreateClient().GetAllExpensesAsync(reportIds);
        }

        public Task UpdateExpenseAsync(Expense expense)
        {
            return _clientFactory.CreateClient().PutAsync(expense);
        }
    }
}

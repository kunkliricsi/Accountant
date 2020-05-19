using Accountant.APP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IExpenseService
    {
        Task<ICollection<Expense>> GetExpensesAsync(params int[] reportIds);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int expenseId);
    }
}

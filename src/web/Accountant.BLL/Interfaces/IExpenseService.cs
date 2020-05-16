using Accountant.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.BLL.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetExpensesAsync(params int[] reportIds);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int expenseId);
    }
}

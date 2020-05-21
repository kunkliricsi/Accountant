using Accountant.APP.Models.Web;
using Accountant.APP.Models.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IExpenseService
    {
        Task<ICollection<Expense>> GetExpensesAsync(params int[] reportIds);
        Task<Expense> CreateExpenseAsync(AddExpenseModel expense);
        Task UpdateExpenseAsync(UpdateExpenseModel expense);
        Task DeleteExpenseAsync(int expenseId);
    }
}

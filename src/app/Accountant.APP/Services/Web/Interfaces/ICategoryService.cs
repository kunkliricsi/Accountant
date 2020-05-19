using Accountant.APP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}

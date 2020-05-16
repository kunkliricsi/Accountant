using Accountant.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}

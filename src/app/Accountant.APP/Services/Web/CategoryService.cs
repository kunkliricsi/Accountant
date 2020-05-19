using Accountant.APP.Models;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoriesClient _client;

        public CategoryService(CategoriesClient client)
        {
            _client = client;
        }

        public Task<Category> CreateCategoryAsync(Category category)
        {
            return _client.PostAsync(category);
        }

        public Task DeleteCategoryAsync(int categoryId)
        {
            return _client.DeleteAsync(categoryId);
        }

        public Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            return _client.GetAllAsync();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            return _client.PutAsync(category);
        }
    }
}

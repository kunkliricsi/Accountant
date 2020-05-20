using Accountant.APP.Models.Web;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class CategoryService : ICategoryService
    {
        private readonly IServiceClientFactory<ICategoriesClient> _clientFactory;

        public CategoryService(IServiceClientFactory<ICategoriesClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<Category> CreateCategoryAsync(Category category)
        {
            return _clientFactory.CreateClient().PostAsync(category);
        }

        public Task DeleteCategoryAsync(int categoryId)
        {
            return _clientFactory.CreateClient().DeleteAsync(categoryId);
        }

        public Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            return _clientFactory.CreateClient().GetAllCategoriesAsync();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            return _clientFactory.CreateClient().PutAsync(category);
        }
    }
}

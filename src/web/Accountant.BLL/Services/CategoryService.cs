using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AccountantContext _context;

        public CategoryService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (_context.Categories.Any(c => c.Name == category.Name))
                throw new EntityAlreadyExistsException($"Category '{category.Name}' already exists.");

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public Task DeleteCategoryAsync(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null)
                return Task.CompletedTask;

            _context.Categories.Remove(category);
            return _context.SaveChangesAsync();
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _context.Categories.ToListAsync();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            var updatedCategory = _context.Categories.Find(category.Id)
                ?? throw new EntityNotFoundException($"Cannot find category with ID: {category.Id}");

            if (!string.IsNullOrWhiteSpace(category.Name) && category.Name != updatedCategory.Name)
            {
                updatedCategory.Name = category.Name;
            }

            if (!string.IsNullOrWhiteSpace(category.Description))
            {
                updatedCategory.Description = category.Description;
            }

            _context.Categories.Update(updatedCategory);
            return _context.SaveChangesAsync();
        }
    }
}

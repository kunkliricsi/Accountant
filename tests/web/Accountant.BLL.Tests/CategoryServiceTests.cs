using Accountant.BLL.Services;
using Accountant.BLL.Tests.Helpers;
using Accountant.DAL.Entities;
using System;
using System.Linq;
using Xunit;

namespace Accountant.BLL.Tests
{
    public class CategoryServiceTests : SqliteTestBase
    {
        private CategoryService Service { get; }
        public CategoryServiceTests()
            : base()
        {
            Service = new CategoryService(DbContext);
        }

        [Fact]
        public void GetAllTest()
        {
            var categories = Service.GetAllCategoriesAsync().Result;
            Assert.False(DbContext.Categories.Any(c => c.Name == "test1"));
            Assert.False(DbContext.Categories.Any(c => c.Name == "test2"));

            DbContext.Categories.Add(new Category { Name = "test1" });
            DbContext.Categories.Add(new Category { Name = "test2" });
            DbContext.SaveChanges();

            categories = Service.GetAllCategoriesAsync().Result;
            Assert.True(DbContext.Categories.Any(c => c.Name == "test1"));
            Assert.True(DbContext.Categories.Any(c => c.Name == "test2"));
        }

        [Fact]
        public void CreateTest()
        {
            Assert.False(DbContext.Categories.Any(c => c.Name == "test"));

            var result = Service.CreateCategoryAsync(new Category { Name = "test" }).Result;

            Assert.Single(DbContext.Categories, c => c.Name == "test");
        }

        [Fact]
        public void UpdateTest()
        {
            var category = new Category { Name = "test1" };
            DbContext.Categories.Add(category);
            DbContext.SaveChanges();

            Assert.True(DbContext.Categories.Any(c => c.Name == "test1"));

            Service.UpdateCategoryAsync(new Category { Id = category.Id, Name = "test2" });

            Assert.False(DbContext.Categories.Any(c => c.Name == "test1"));
            Assert.True(DbContext.Categories.Any(c => c.Name == "test2"));
        }

        [Fact]
        public void DeleteTest()
        {
            var category = new Category { Name = "test1" };
            DbContext.Categories.Add(category);
            DbContext.SaveChanges();

            Assert.True(DbContext.Categories.Any(c => c.Name == "test1"));

            Service.DeleteCategoryAsync(category.Id);

            Assert.False(DbContext.Categories.Any(c => c.Name == "test1"));
        }
    }
}

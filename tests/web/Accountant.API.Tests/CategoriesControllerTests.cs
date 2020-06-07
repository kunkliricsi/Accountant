using Accountant.API.Controllers;
using Accountant.API.DTOs;
using Accountant.BLL.Interfaces;
using Accountant.BLL.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Accountant.API.Tests
{
    public class CategoriesControllerTests
    {
        private Mock<ICategoryService> ServiceMock { get; }
        private Mock<ILogger<CategoriesController>> LoggerMock { get; }
        private Mock<IMapper> MapperMock { get; }
        private CategoriesController Controller { get; }

        public CategoriesControllerTests()
        {
            var category = new DAL.Entities.Category { Id = 1, Name = "test", Description = "testDesc" };

            ServiceMock = new Mock<ICategoryService>();
            ServiceMock.Setup(s => s.GetAllCategoriesAsync()).Returns(Task.FromResult(new List<DAL.Entities.Category> { category }));
            ServiceMock.Setup(s => s.CreateCategoryAsync(It.IsAny<DAL.Entities.Category>())).Returns(Task.FromResult(category));
            ServiceMock.Setup(s => s.UpdateCategoryAsync(It.IsAny<DAL.Entities.Category>())).Returns(Task.FromResult(category));

            LoggerMock = new Mock<ILogger<CategoriesController>>();

            MapperMock = new Mock<IMapper>();

            Controller = new CategoriesController(ServiceMock.Object, MapperMock.Object, LoggerMock.Object);
        }

        [Fact]
        public void GetAllTest()
        {
            Controller.GetAllCategoriesAsync().Wait();

            ServiceMock.Verify(s => s.GetAllCategoriesAsync(), Times.Once());
        }

        [Fact]
        public void PostTest()
        {
            var category = new Category { Name = "test" };

            Controller.PostAsync(category).Wait();

            MapperMock.Verify(m => m.Map<DAL.Entities.Category>(category), Times.Once());
            ServiceMock.Verify(s => s.CreateCategoryAsync(It.IsAny<DAL.Entities.Category>()), Times.Once());
        }

        [Fact]
        public void PutTest()
        {
            var category = new Category { Name = "test" };

            Controller.PutAsync(category).Wait();

            MapperMock.Verify(m => m.Map<DAL.Entities.Category>(category), Times.Once());
            ServiceMock.Verify(s => s.UpdateCategoryAsync(It.IsAny<DAL.Entities.Category>()), Times.Once());
        }

        [Fact]
        public void DeleteTest()
        {
            var rand = new Random().Next(1, 100);

            Controller.DeleteAsync(rand).Wait();

            ServiceMock.Verify(s => s.DeleteCategoryAsync(rand), Times.Once());
        }
    }
}

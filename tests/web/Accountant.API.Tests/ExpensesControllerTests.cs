using Accountant.API.Controllers;
using Accountant.API.Tests.Helpers;
using Accountant.BLL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Accountant.API.Tests
{
    public class ExpensesControllerTests
    {
        private object category;

        private Mock<IExpenseService> ServiceMock { get; }
        private Mock<ILogger<ExpensesController>> LoggerMock { get; }
        private Mock<IMapper> MapperMock { get; }
        private ExpensesController Controller { get; }

        public ExpensesControllerTests()
        {
            var expense = new DAL.Entities.Expense { Id = 1, Amount = 1000, CategoryId = 1, UserId = 1, ReportId = 1, PurchaseDate = DateTime.Now };

            ServiceMock = new Mock<IExpenseService>();
            ServiceMock.Setup(s => s.GetExpensesAsync()).Returns(Task.FromResult(new List<DAL.Entities.Expense> { expense }));
            ServiceMock.Setup(s => s.CreateExpenseAsync(It.IsAny<DAL.Entities.Expense>())).Returns(Task.FromResult(expense));
            ServiceMock.Setup(s => s.UpdateExpenseAsync(It.IsAny<DAL.Entities.Expense>())).Returns(Task.FromResult(expense));

            LoggerMock = new Mock<ILogger<ExpensesController>>();

            MapperMock = new Mock<IMapper>();

            Controller = new ExpensesController(ServiceMock.Object, MapperMock.Object, LoggerMock.Object);
        }

        [Fact]
        public void GetTest()
        {
            Controller.GetAllExpensesAsync(new int[] { 1 }).Wait();

            ServiceMock.Verify(s => s.GetExpensesAsync(It.IsAny<int[]>()), Times.Once());
        }

        [Fact]
        public void PostTest()
        {
            var model = new DTOs.AddOrUpdateModels.AddExpenseModel { Amount = 1000, CategoryId = 1, ReportId = 1, UserId = 1 };

            Controller.PostAsync(model).Wait();

            MapperMock.Verify(m => m.Map<DAL.Entities.Expense>(model), Times.Once());
            ServiceMock.Verify(s => s.CreateExpenseAsync(It.IsAny<DAL.Entities.Expense>()), Times.Once());
        }

        [Fact]
        public void PutTest()
        {
            var model = new DTOs.AddOrUpdateModels.UpdateExpenseModel { Id = 1, Amount = 1000, CategoryId = 1, ReportId = 1, UserId = 1 };

            Controller.PutAsync(model).Wait();

            MapperMock.Verify(m => m.Map<DAL.Entities.Expense>(model), Times.Once());
            ServiceMock.Verify(s => s.UpdateExpenseAsync(It.IsAny<DAL.Entities.Expense>()), Times.Once());
        }

        [Theory]
        [MemberData(nameof(RangeData.Data), MemberType = typeof(RangeData))]
        public void DeleteTest(int id)
        {
            Controller.DeleteAsync(id).Wait();

            ServiceMock.Verify(s => s.DeleteExpenseAsync(id), Times.Once());
        }
    }
}

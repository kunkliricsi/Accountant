using Accountant.BLL.Services;
using Accountant.BLL.Tests.Base;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Accountant.BLL.Tests
{
    public class ShoppingListServiceTests : SqliteTestBase
    {
        private ShoppingListService Service { get; }

        public ShoppingListServiceTests()
            : base()
        {
            Service = new ShoppingListService(DbContext);
        }

        [Fact]
        public void GetListTest()
        {
            var list = new ShoppingList { GroupId = 1, Name = "test" };
            DbContext.ShoppingLists.Add(list);
            DbContext.SaveChanges();

            var result = Service.GetShoppingListAsync(list.Id).Result;

            Assert.Equal(list, result);
        }

        [Fact]
        public void CreateListTest()
        {
            var list = new ShoppingList { Name = "test", GroupId = 1 };

            Assert.DoesNotContain(list, DbContext.ShoppingLists);

            Service.CreateShoppingListAsync(list).Wait();

            Assert.Contains(list, DbContext.ShoppingLists);
        }

        [Fact]
        public void UpdateListTest()
        {
            var list = new ShoppingList { Name = "test", GroupId = 1 };
            DbContext.ShoppingLists.Add(list);
            DbContext.SaveChanges();

            list.Name = "test2";
            Service.UpdateShoppingListAsync(list).Wait();

            Assert.True(DbContext.ShoppingLists.Any(l => l.Name == "test2"));
        }

        [Fact]
        public void DeleteListTest()
        {
            var list = new ShoppingList { Name = "test", GroupId = 1 };
            DbContext.ShoppingLists.Add(list);
            DbContext.SaveChanges();

            Service.DeleteShoppingListAsync(list.Id);

            Assert.DoesNotContain(list, DbContext.ShoppingLists);
        }

        [Fact]
        public void CreateItemTest()
        {
            var list = new ShoppingList { Name = "test", GroupId = 1 };
            DbContext.ShoppingLists.Add(list);
            DbContext.SaveChanges();

            var item = new ShoppingListItem { Name = "testItem", ShoppingListId = list.Id };
            Service.CreateShoppingListItemAsync(item).Wait();

            Assert.Contains(item, DbContext.ShoppingListItems);

            var result = DbContext.ShoppingLists.Find(list.Id);
            Assert.Contains(item, result.ShoppingListItems);
        }

        [Fact]
        public void UpdateItemTest()
        {
            var list = new ShoppingList { Name = "test", GroupId = 1 };
            var item = new ShoppingListItem { Name = "testItem", ShoppingListId = list.Id };
            list.ShoppingListItems.Add(item);
            DbContext.ShoppingLists.Add(list);
            DbContext.SaveChanges();

            item.Name = "testItem2";
            Service.UpdateShoppingListItemAsync(item).Wait();

            Assert.True(DbContext.ShoppingListItems.Any(i => i.Name == "testItem2"));
        }

        [Fact]
        public void DeleteItemTest()
        {
            var list = new ShoppingList { Name = "test", GroupId = 1 };
            var item = new ShoppingListItem { Name = "testItem", ShoppingListId = list.Id };
            list.ShoppingListItems.Add(item);
            DbContext.ShoppingLists.Add(list);
            DbContext.SaveChanges();

            Service.DeleteShoppingListItemAsync(item.Id);

            Assert.DoesNotContain(item, DbContext.ShoppingListItems);
        }
    }
}

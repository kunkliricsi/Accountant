using Accountant.APP.Models;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.Services.Web.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ShoppingListsClient _client;

        public ShoppingListService(ShoppingListsClient client)
        {
            _client = client;
        }

        public Task<ShoppingList> CreateShoppingListAsync(ShoppingList shoppingList, int groupId)
        {
            return _client.PostAsync(shoppingList, groupId);
        }

        public Task<ShoppingListItem> CreateShoppingListItemAsync(ShoppingListItem listItem)
        {
            return _client.Post2Async(listItem);
        }

        public Task DeleteShoppingListAsync(int shoppingListId)
        {
            return _client.DeleteAsync(shoppingListId);
        }

        public Task DeleteShoppingListItemAsync(int listItemId)
        {
            return _client.DeleteItemAsync(listItemId);
        }

        public Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            return _client.GetAsync(shoppingListId);
        }

        public Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            return _client.PutAsync(shoppingList);
        }

        public Task UpdateShoppingListItemAsync(ShoppingListItem listItem)
        {
            return _client.Put2Async(listItem);
        }
    }
}

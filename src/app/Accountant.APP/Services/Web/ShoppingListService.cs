using Accountant.APP.Models.Web;
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
        private readonly IServiceClientFactory<IShoppingListsClient> _clientFactory;

        public ShoppingListService(IServiceClientFactory<IShoppingListsClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<ShoppingList> CreateShoppingListAsync(ShoppingList shoppingList, int groupId)
        {
            return _clientFactory.CreateClient().PostAsync(shoppingList, groupId);
        }

        public Task<ShoppingListItem> CreateShoppingListItemAsync(ShoppingListItem listItem)
        {
            return _clientFactory.CreateClient().Post2Async(listItem);
        }

        public Task DeleteShoppingListAsync(int shoppingListId)
        {
            return _clientFactory.CreateClient().DeleteAsync(shoppingListId);
        }

        public Task DeleteShoppingListItemAsync(int listItemId)
        {
            return _clientFactory.CreateClient().DeleteItemAsync(listItemId);
        }

        public Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            return _clientFactory.CreateClient().GetShoppingListAsync(shoppingListId);
        }

        public Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            return _clientFactory.CreateClient().PutAsync(shoppingList);
        }

        public Task UpdateShoppingListItemAsync(ShoppingListItem listItem)
        {
            return _clientFactory.CreateClient().Put2Async(listItem);
        }
    }
}

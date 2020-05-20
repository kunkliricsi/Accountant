using Accountant.APP.Models.Web;
using System.Threading.Tasks;

namespace Accountant.APP.Services.Web.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingList> GetShoppingListAsync(int shoppingListId);
        Task<ShoppingList> CreateShoppingListAsync(ShoppingList shoppingList, int groupId);
        Task UpdateShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListAsync(int shoppingListId);

        Task<ShoppingListItem> CreateShoppingListItemAsync(ShoppingListItem listItem);
        Task UpdateShoppingListItemAsync(ShoppingListItem listItem);
        Task DeleteShoppingListItemAsync(int listItemId);
    }
}

using Accountant.BLL.Exceptions;
using Accountant.BLL.Interfaces;
using Accountant.DAL;
using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.BLL.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly AccountantContext _context;

        public ShoppingListService(AccountantContext context)
        {
            _context = context;
        }

        public async Task<ShoppingList> CreateShoppingListAsync(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Add(shoppingList);
            await _context.SaveChangesAsync();

            return shoppingList;
        }

        public async Task<ShoppingListItem> CreateShoppingListItemAsync(ShoppingListItem listItem)
        {
            _context.ShoppingListItems.Add(listItem);
            await _context.SaveChangesAsync();

            return listItem;
        }

        public Task DeleteShoppingListAsync(int shoppingListId)
        {
            var shoppingList = _context.ShoppingLists.Find(shoppingListId);

            if (shoppingList == null)
                return Task.CompletedTask;

            _context.ShoppingLists.Remove(shoppingList);
            return _context.SaveChangesAsync();
        }

        public Task DeleteShoppingListItemAsync(int listItemId)
        {
            var listItem = _context.ShoppingListItems.Find(listItemId);

            if (listItem == null)
                return Task.CompletedTask;

            _context.ShoppingListItems.Remove(listItem);
            return _context.SaveChangesAsync();
        }

        public Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            return _context.ShoppingLists
                .Include(sl => sl.ShoppingListItems)
                .SingleOrDefaultAsync(sl => sl.Id == shoppingListId)
                ?? throw new EntityNotFoundException($"Cannot find shopping list with ID: {shoppingListId}");
        }

        public Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            var updatedShoppingList = _context.ShoppingLists.Find(shoppingList.Id)
                ?? throw new EntityNotFoundException("Cannot find shopping list with ID: {shoppingList.Id}");

            if (!string.IsNullOrWhiteSpace(shoppingList.Name))
            {
                updatedShoppingList.Name = shoppingList.Name;
            }

            _context.ShoppingLists.Update(updatedShoppingList);
            return _context.SaveChangesAsync();
        }

        public Task UpdateShoppingListItemAsync(ShoppingListItem listItem)
        {
            var updatedListItem = _context.ShoppingListItems.Find(listItem.Id)
                ?? throw new EntityNotFoundException("Cannot find shopping list item with ID: {listItem.Id}");

            if (!string.IsNullOrWhiteSpace(listItem.Name))
            {
                updatedListItem.Name = listItem.Name;
            }

            if (listItem.IsTicked != null)
            {
                updatedListItem.IsTicked = listItem.IsTicked;
            }

            _context.ShoppingListItems.Update(updatedListItem);
            return _context.SaveChangesAsync();
        }
    }
}

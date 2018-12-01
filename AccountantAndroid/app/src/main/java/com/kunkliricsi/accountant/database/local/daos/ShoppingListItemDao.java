package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

@Dao
public interface ShoppingListItemDao {

    @Query("SELECT * FROM shoppinglist")
    public ShoppingListItem[] getShoppingList();

    @Query("SELECT * FROM shoppinglist WHERE id = :id")
    public ShoppingListItem getShoppingListItem(int id);

    @Query("SELECT * FROM shoppinglist ORDER BY id DESC LIMIT 1")
    public ShoppingListItem getLastShoppingListItem();

    @Query("SELECT * FROM shoppinglist WHERE expense = :id")
    public ShoppingListItem[] getShoppingListOfExpense(int id);

    @Query("SELECT * FROM shoppinglist WHERE expense IS NULL")
    public ShoppingListItem[] getAllUnpaidShoppingListItems();

    @Update
    public void updateShoppingListItem(ShoppingListItem... items);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addShoppingListItem(ShoppingListItem... items);

    @Delete
    public void deleteShoppingListItem(ShoppingListItem... items);

    @Query("DELETE FROM shoppinglist")
    public void deleteAll();
}

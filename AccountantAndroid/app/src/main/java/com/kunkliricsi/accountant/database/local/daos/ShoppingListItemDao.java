package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.OnConflictStrategy;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

@Dao
public interface ShoppingListItemDao {

    @Query("SELECT * FROM shoppinglist")
    public ShoppingListItem[] getShoppingList();

    @Query("SELECT * FROM shoppinglist WHERE name = :name AND expense IS NULL")
    public ShoppingListItem getShoppingListItemByName(String name);

    @Query("SELECT * FROM shoppinglist WHERE id = :id")
    public ShoppingListItem getShoppingListItem(int id);

    @Query("SELECT * FROM shoppinglist ORDER BY id DESC LIMIT 1")
    public ShoppingListItem getLastShoppingListItem();

    @Query("SELECT * FROM shoppinglist WHERE expense = :id")
    public ShoppingListItem[] getShoppingListOfExpense(int id);

    @Query("SELECT * FROM shoppinglist WHERE expense IS NULL")
    public ShoppingListItem[] getAllUnpaidShoppingListItems();

    @Query("SELECT name FROM shoppinglist WHERE expense IS NULL")
    public String[] getAllUnpaidShoppingListItemNames();

    @Update
    public void updateShoppingListItem(ShoppingListItem... items);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addShoppingListItem(ShoppingListItem... items);

    @Delete
    public void deleteShoppingListItem(ShoppingListItem... items);

    @Query("DELETE FROM shoppinglist")
    public void deleteAll();
}

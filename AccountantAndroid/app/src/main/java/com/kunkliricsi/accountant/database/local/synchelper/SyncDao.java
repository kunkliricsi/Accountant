package com.kunkliricsi.accountant.database.local.synchelper;

import com.kunkliricsi.accountant.database.local.entities.Expense;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Query;

@Dao
public interface SyncDao {

    @Insert
    public void addCategory(CategorySync... categories);

    @Query("DELETE FROM categorysync WHERE post = :id")
    public void deletePostCategory(int id);

    @Query("DELETE FROM categorysync WHERE `delete` = :id")
    public void deleteDeleteCategory(int id);

    @Insert
    public void addExpense(ExpenseSync... expenses);

    @Query("DELETE FROM expensesync WHERE post = :id")
    public void deletePostExpense(int id);

    @Query("DELETE FROM expensesync WHERE put = :id")
    public void deletePutExpense(int id);

    @Insert
    public void addReport(ReportSync... reports);

    @Query("DELETE FROM reportsync WHERE put = :id")
    public void deletePutReport(int id);

    @Query("DELETE FROM reportsync WHERE post = :id")
    public void deletePostReport(int id);

    @Insert
    public void addShoppingListItem(ShoppingListItemSync... items);

    @Query("DELETE FROM shoppinglistsync WHERE put = :id")
    public void deletePutShoppingListItem(int id);

    @Query("DELETE FROM shoppinglistsync WHERE post = :id")
    public void deletePostShoppingListItem(int id);

    @Insert
    public void addUser(UserSync... users);

    @Query("DELETE FROM usersync WHERE post = :id")
    public void deletePostUser(int id);

    @Query("DELETE FROM usersync WHERE put = :id")
    public void deletePutUser(int id);


    @Query("SELECT post FROM categorysync WHERE post IS NOT NULL")
    public int[] getPostCategories();

    @Query("SELECT `delete` FROM categorysync WHERE `delete` IS NOT NULL")
    public int[] getDeleteCategories();

    @Query("SELECT post FROM expensesync WHERE post IS NOT NULL")
    public int[] getPostExpenses();

    @Query("SELECT put FROM expensesync WHERE put IS NOT NULL")
    public int[] getPutExpenses();

    @Query("SELECT post FROM reportsync WHERE post IS NOT NULL")
    public int[] getPostReports();

    @Query("SELECT put FROM reportsync WHERE put IS NOT NULL")
    public int[] getPutReports();

    @Query("SELECT post FROM shoppinglistsync WHERE post IS NOT NULL")
    public int[] getPostShoppingList();

    @Query("SELECT put FROM shoppinglistsync WHERE put IS NOT NULL")
    public int[] getPutShoppingList();

    @Query("SELECT post FROM usersync WHERE post IS NOT NULL")
    public int[] getPostUsers();

    @Query("SELECT put FROM usersync WHERE put IS NOT NULL")
    public int[] getPutUsers();
}

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

    @Delete
    public void deleteCategory(CategorySync... categories);

    @Insert
    public void addExpense(ExpenseSync... expenses);

    @Delete
    public void deleteExpense(ExpenseSync... expenses);

    @Insert
    public void addReport(ReportSync... reports);

    @Delete
    public void deleteReport(ReportSync... reports);

    @Insert
    public void addShoppingListItem(ShoppingListItemSync... items);

    @Delete
    public void deleteShoppingListItem(ShoppingListItemSync... items);

    @Insert
    public void addUser(UserSync... users);

    @Delete
    public void deleteUser(UserSync... users);


    @Query("SELECT post FROM categorysync WHERE post IS NOT NULL")
    public CategorySync[] getPostCategories();

    @Query("SELECT `delete` FROM categorysync WHERE `delete` IS NOT NULL")
    public CategorySync[] getDeleteCategories();

    @Query("SELECT post FROM expensesync WHERE post IS NOT NULL")
    public ExpenseSync[] getPostExpenses();

    @Query("SELECT put FROM expensesync WHERE put IS NOT NULL")
    public ExpenseSync[] getPutExpenses();

    @Query("SELECT post FROM reportsync WHERE post IS NOT NULL")
    public ReportSync[] getPostReports();

    @Query("SELECT put FROM reportsync WHERE put IS NOT NULL")
    public ReportSync[] getPutReports();

    @Query("SELECT post FROM shoppinglistsync WHERE post IS NOT NULL")
    public ShoppingListItemSync[] getPostShoppingList();

    @Query("SELECT put FROM shoppinglistsync WHERE put IS NOT NULL")
    public ShoppingListItemSync[] getPutShoppingList();

    @Query("SELECT post FROM usersync WHERE post IS NOT NULL")
    public UserSync[] getPostUsers();

    @Query("SELECT put FROM usersync WHERE put IS NOT NULL")
    public UserSync[] getPutUsers();


}

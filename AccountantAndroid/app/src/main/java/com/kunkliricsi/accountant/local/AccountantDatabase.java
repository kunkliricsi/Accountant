package com.kunkliricsi.accountant.local;

import com.kunkliricsi.accountant.local.daos.CategoryDao;
import com.kunkliricsi.accountant.local.daos.ChangesDao;
import com.kunkliricsi.accountant.local.daos.ExpenseDao;
import com.kunkliricsi.accountant.local.daos.ReportDao;
import com.kunkliricsi.accountant.local.daos.ShoppingListItemDao;
import com.kunkliricsi.accountant.local.daos.UserDao;
import com.kunkliricsi.accountant.local.entities.Category;
import com.kunkliricsi.accountant.local.entities.Changes;
import com.kunkliricsi.accountant.local.entities.Expense;
import com.kunkliricsi.accountant.local.entities.Report;
import com.kunkliricsi.accountant.local.entities.ShoppingListItem;
import com.kunkliricsi.accountant.local.entities.User;

import androidx.room.Database;
import androidx.room.RoomDatabase;

@Database(version = 1, entities = {Category.class, Changes.class, Expense.class, Report.class, ShoppingListItem.class, User.class})
public abstract class AccountantDatabase extends RoomDatabase {

    public abstract CategoryDao categoryDao();
    public abstract ChangesDao changesDao();
    public abstract ExpenseDao expenseDao();
    public abstract ReportDao reportDao();
    public abstract ShoppingListItemDao shoppingListItemDao();
    public abstract UserDao userDao();

}

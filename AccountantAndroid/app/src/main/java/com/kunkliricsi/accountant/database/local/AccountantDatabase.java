package com.kunkliricsi.accountant.database.local;

import android.content.Context;

import com.kunkliricsi.accountant.database.local.daos.CategoryDao;
import com.kunkliricsi.accountant.database.local.daos.ChangesDao;
import com.kunkliricsi.accountant.database.local.daos.ExpenseDao;
import com.kunkliricsi.accountant.database.local.daos.ReportDao;
import com.kunkliricsi.accountant.database.local.daos.ShoppingListItemDao;
import com.kunkliricsi.accountant.database.local.daos.UserDao;
import com.kunkliricsi.accountant.database.local.entities.Category;
import com.kunkliricsi.accountant.database.local.entities.Changes;
import com.kunkliricsi.accountant.database.local.entities.Expense;
import com.kunkliricsi.accountant.database.local.entities.Report;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;
import com.kunkliricsi.accountant.database.local.entities.User;

import android.arch.persistence.room.Database;
import android.arch.persistence.room.Room;
import android.arch.persistence.room.RoomDatabase;

@Database(version = 1, exportSchema = false, entities = {Category.class, Changes.class, Expense.class, Report.class, ShoppingListItem.class, User.class})
public abstract class AccountantDatabase extends RoomDatabase {

    public abstract CategoryDao categoryDao();
    public abstract ChangesDao changesDao();
    public abstract ExpenseDao expenseDao();
    public abstract ReportDao reportDao();
    public abstract ShoppingListItemDao shoppingListItemDao();
    public abstract UserDao userDao();

    private static AccountantDatabase instance;

    public static synchronized AccountantDatabase getInstance(Context context) {
        if (instance == null) {
            instance = Room.databaseBuilder(context.getApplicationContext(), AccountantDatabase.class, "Accountant").allowMainThreadQueries().build();
        }

        return instance;
    }

}

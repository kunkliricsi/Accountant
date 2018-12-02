package com.kunkliricsi.accountant.database;

import android.accounts.Account;
import android.content.AbstractThreadedSyncAdapter;
import android.content.ContentProviderClient;
import android.content.ContentResolver;
import android.content.Context;
import android.content.SyncResult;
import android.os.Bundle;
import android.widget.CursorAdapter;
import android.widget.Toast;

import com.kunkliricsi.accountant.MainActivity;
import com.kunkliricsi.accountant.database.local.AccountantDatabase;
import com.kunkliricsi.accountant.database.local.entities.Category;
import com.kunkliricsi.accountant.database.local.entities.Changes;
import com.kunkliricsi.accountant.database.local.entities.Expense;
import com.kunkliricsi.accountant.database.local.entities.Report;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;
import com.kunkliricsi.accountant.database.local.entities.User;
import com.kunkliricsi.accountant.database.local.synchelper.SyncDatabase;
import com.kunkliricsi.accountant.database.network.NetworkManager;

import java.net.UnknownServiceException;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SyncAdapter extends AbstractThreadedSyncAdapter {

    AccountantDatabase database;
    SyncDatabase syncDatabase;
    NetworkManager manager;

    public SyncAdapter(Context context, boolean autoInitialize) {
        super(context, autoInitialize);

        database = AccountantDatabase.getInstance(context);
        syncDatabase = SyncDatabase.getInstance(context);
        manager = NetworkManager.getInstance();
    }

    @Override
    public void onPerformSync(Account account, Bundle extras, String authority, ContentProviderClient provider, SyncResult syncResult) {
        try {
            Changes local = database.changesDao().getChanges();
            Call<Changes> call = manager.getChanges();
            Changes server = call.execute().body();

            if (local.lastModified.compareTo(server.lastModified) != 0 && syncChanges(local, server)) {
                Call<Changes> update = manager.getChanges();
                database.changesDao().updateChanges(update.execute().body());
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        } finally {
            database.close();
            syncDatabase.close();
        }
    }

    private boolean syncChanges(Changes local, Changes server) {

        try {

            if (local.Category.compareTo(server.Category) != 0) {
                syncCategories();
            }

            if (local.Expense.compareTo(server.Expense) != 0) {
                syncExpenses();
            }

            if (local.Report.compareTo(server.Report) != 0) {
                syncReports();
            }

            if (local.ShoppingListItem.compareTo(server.ShoppingListItem) != 0) {
                syncShoppingList();
            }

            if (local.User.compareTo(server.User) != 0) {
                syncUsers();
            }

        } catch (Exception ex) {
            return false;
        }

        return true;
    }

    private void syncCategories() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostCategories();
        for (int id : postIds) {
            Category c = database.categoryDao().getCategory(id);
            c.id = 0;
            manager.addCategory(c).execute();
            syncDatabase.Dao().deletePostCategory(id);
        }

        int[] deleteIds = syncDatabase.Dao().getDeleteCategories();
        for (int id : deleteIds) {
            manager.deleteCategory(id);
            syncDatabase.Dao().deleteDeleteCategory(id);
        }

        database.categoryDao().deleteAll();
        List<Category> cs = manager.getCategories().execute().body();
        for (Category c : cs ) {
            database.categoryDao().addCategory(c);
        }
    }

    private void syncExpenses() throws Exception{
        int[] postIds = syncDatabase.Dao().getPostExpenses();
        for (int id : postIds) {
            Expense e = database.expenseDao().getExpense(id);
            e.id = 0;
            manager.addExpense(e).execute();
            syncDatabase.Dao().deletePostExpense(id);
        }

        int[] putIds = syncDatabase.Dao().getPutExpenses();
        for (int id : putIds) {
            Expense e = database.expenseDao().getExpense(id);
            manager.updateExpense(e.id, e);
            syncDatabase.Dao().deletePutExpense(id);
        }

        database.expenseDao().deleteAll();
        List<Expense> es = manager.getExpenses().execute().body();
        for (Expense e : es) {
            database.expenseDao().addExpense(e);
        }
    }

    private void syncReports() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostReports();
        for (int id : postIds) {
            Report r = database.reportDao().getReport(id);
            r.id = 0;
            manager.addReport(r);
            syncDatabase.Dao().deletePostReport(id);
        }

        int[] putIds = syncDatabase.Dao().getPutReports();
        for (int id : putIds) {
            Report r = database.reportDao().getReport(id);
            manager.updateReport(r.id, r);
            syncDatabase.Dao().deletePutReport(id);
        }

        database.reportDao().deleteAll();
        List<Report> rs = manager.getReports().execute().body();
        for (Report r : rs) {
            database.reportDao().addReport(r);
        }
    }

    private void syncShoppingList() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostShoppingList();
        for (int id : postIds) {
            ShoppingListItem s = database.shoppingListItemDao().getShoppingListItem(id);
            s.id = 0;
            manager.addShoppingListItem(s);
            syncDatabase.Dao().deletePostShoppingListItem(id);
        }

        int[] putIds = syncDatabase.Dao().getPutShoppingList();
        for (int id : putIds) {
            ShoppingListItem s = database.shoppingListItemDao().getShoppingListItem(id);
            manager.updateShoppingListItem(s.id, s);
            syncDatabase.Dao().deletePutShoppingListItem(id);
        }

        database.shoppingListItemDao().deleteAll();
        List<ShoppingListItem> ss = manager.getShoppingList().execute().body();
        for (ShoppingListItem s : ss) {
            database.shoppingListItemDao().addShoppingListItem(s);
        }
    }

    private void syncUsers() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostUsers();
        for (int id : postIds) {
            User u = database.userDao().getUser(id);
            u.id = 0;
            manager.addUser(u);
            syncDatabase.Dao().deletePostUser(id);
        }

        int[] putIds = syncDatabase.Dao().getPutUsers();
        for (int id : putIds) {
            User u = database.userDao().getUser(id);
            manager.updateUser(u.id, u);
            syncDatabase.Dao().deletePutUser(id);
        }

        database.userDao().deleteAll();
        List<User> us = manager.getUsers().execute().body();
        for (User u : us) {
            database.userDao().addUser(u);
        }
    }
}

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
import com.kunkliricsi.accountant.database.network.NetworkManager;

import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SyncAdapter extends AbstractThreadedSyncAdapter {

    AccountantDatabase database;
    NetworkManager manager;

    public SyncAdapter(Context context, boolean autoInitialize) {
        super(context, autoInitialize);

        database = AccountantDatabase.getInstance(context);
        manager = NetworkManager.getInstance();
    }

    @Override
    public void onPerformSync(Account account, Bundle extras, String authority, ContentProviderClient provider, SyncResult syncResult) {
        Changes local = database.changesDao().getChanges();

        try {
            Call<Changes> call = manager.getChanges();
            Changes change = call.execute().body();

            if (local.lastModified.compareTo(change.lastModified) != 0)) {
                syncChanges(change);
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }

    }

    private void syncChanges(Changes change) {
        Changes localChange = database.changesDao().getChanges();

        try {

            if (localChange.Category.compareTo(change.Category) != 0) {
                Call<List<Category>> call = manager.getCategories();
                List<Category> categories = call.execute().body();
                syncCategories(categories);
            }

            if (localChange.Expense.compareTo(change.Expense) != 0) {
                Call<List<Expense>> call = manager.getExpenses();
                List<Expense> expenses = call.execute().body();
            }

            if (localChange.Report.compareTo(change.Report) != 0) {
                Call<List<Report>> call = manager.getReports();
            }

            if (localChange.ShoppingListItem.compareTo(change.ShoppingListItem) != 0) {
                Call<List<ShoppingListItem>> call = manager.getShoppingList();
            }

            if (localChange.User.compareTo(change.User) != 0) {
                Call<List<User>> call = manager.getUsers();
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private void syncCategories(List<Category> categories) {
        Category[] local = database.categoryDao().getAllCategories();
    }

    private void syncExpenses(List<Expense> expenses) {

    }

    private void syncReports(List<Report> reports) {

    }

    private void syncShoppingList(List<ShoppingListItem> shoppinglist) {

    }

    private void syncUsers(List<User> users) {

    }
}

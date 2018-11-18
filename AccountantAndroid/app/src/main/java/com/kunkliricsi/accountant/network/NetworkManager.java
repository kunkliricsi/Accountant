package com.kunkliricsi.accountant.network;

import android.app.ApplicationErrorReport;
import android.net.Network;

import com.kunkliricsi.accountant.model.Category;
import com.kunkliricsi.accountant.model.Changes;
import com.kunkliricsi.accountant.model.Expense;
import com.kunkliricsi.accountant.model.Report;
import com.kunkliricsi.accountant.model.ShoppingListItem;
import com.kunkliricsi.accountant.model.User;

import java.util.List;

import okhttp3.OkHttpClient;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class NetworkManager {

    private static final String SERVICE_URL = "http://kunkliricsi.dynv6.net:8000";

    private static NetworkManager instance;

    public static NetworkManager getInstance() {
        if (instance == null) {
            instance = new NetworkManager();
        }
        return instance;
    }

    private Retrofit retrofit;
    private AccountantApi api;

    private NetworkManager() {
        retrofit = new Retrofit.Builder()
                .baseUrl(SERVICE_URL)
                .client(new OkHttpClient.Builder().build())
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        api = retrofit.create(AccountantApi.class);
    }

    public Call<Changes> getChanges() {
        return api.getChanges();
    }

    public Call<List<Category>> getCategories() {
        return api.getCategories();
    }

    public Call<Category> getCategory(int ID) {
        return api.getCategory(ID);
    }

    public Call<ResponseBody> createCategory(Category category) {
        return api.createCategory(category);
    }

    public Call<ResponseBody> deleteCategory(int ID) {
        return api.deleteCategory(ID);
    }

    public Call<List<Expense>> getExpenses() {
        return api.getExpenses();
    }

    public Call<Expense> getExpense(int ID) {
        return api.getExpense(ID);
    }

    public Call<ResponseBody> addExpense(Expense expense) {
        return api.addExpense(expense);
    }

    public Call<ResponseBody> updateExpense(int ID, Expense expense) {
        return api.updateExpense(ID, expense);
    }

    public Call<List<Report>> getReports() {
        return api.getReports();
    }

    public Call<Report> getReport(int ID) {
        return api.getReport(ID);
    }

    public Call<ResponseBody> addReport(Report report) {
        return  api.addReport(report);
    }

    public Call<ResponseBody> updateReport(int ID, Report report) {
        return api.updateReport(ID, report);
    }

    public Call<List<ShoppingListItem>> getShoppingList() {
        return api.getShoppingList();
    }

    public Call<ShoppingListItem> getShoppingListItem(int ID) {
        return api.getShoppingListItem(ID);
    }

    public Call<ResponseBody> addShoppingListItem(ShoppingListItem item) {
        return api.addShoppingListItem(item);
    }

    public Call<ResponseBody> updateShoppingListItem(int ID, ShoppingListItem item) {
        return api.updateShoppingListItem(ID, item);
    }

    public Call<ResponseBody> deleteShoppingListItem(int ID) {
        return api.deleteShoppingListItem(ID);
    }

    public Call<List<User>> getUsers() {
        return api.getUsers();
    }

    public Call<User> getUser(int ID) {
        return api.getUser(ID);
    }

    public Call<ResponseBody> addUser(User user) {
        return api.addUser(user);
    }

    public Call<ResponseBody> updateUser(int ID, User user) {
        return api.updateUser(ID, user);
    }













}

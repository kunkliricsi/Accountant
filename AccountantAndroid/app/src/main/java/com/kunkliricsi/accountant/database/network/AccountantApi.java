package com.kunkliricsi.accountant.database.network;

import com.kunkliricsi.accountant.database.local.entities.Category;
import com.kunkliricsi.accountant.database.local.entities.Changes;
import com.kunkliricsi.accountant.database.local.entities.Expense;
import com.kunkliricsi.accountant.database.local.entities.Report;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;
import com.kunkliricsi.accountant.database.local.entities.User;

import java.util.List;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface AccountantApi {

    @GET()
    Call<Changes> getChanges();


    @GET("/category")
    Call<List<Category>> getCategories();

    @GET("/category/{id}")
    Call<Category> getCategory(@Path("id") int ID);

    @POST("/category")
    Call<ResponseBody> addCategory(@Body Category category);

    @DELETE("/category/{id}")
    Call<ResponseBody> deleteCategory(@Path("id") int ID);


    @GET("/expense")
    Call<List<Expense>> getExpenses();

    @GET("/expense/{id}")
    Call<Expense> getExpense(@Path("id") int ID);

    @POST("/expense")
    Call<ResponseBody> addExpense(@Body Expense expense);

    @PUT("/expense/{id}")
    Call<ResponseBody> updateExpense(@Path("id") int ID, @Body Expense expense);


    @GET("/report")
    Call<List<Report>> getReports();

    @GET("/report/{id}")
    Call<Report> getReport(@Path("id") int ID);

    @POST("/report")
    Call<ResponseBody> addReport(@Body Report report);

    @PUT("/report/{id}")
    Call<ResponseBody> updateReport(@Path("id") int ID, @Body Report report);


    @GET("/shoppinglist")
    Call<List<ShoppingListItem>> getShoppingList();

    @GET("/shoppinglist/{id}")
    Call<ShoppingListItem> getShoppingListItem(@Path("id") int ID);

    @POST("/shoppinglist")
    Call<ResponseBody> addShoppingListItem(@Body ShoppingListItem item);

    @PUT("/shoppinglist/{id}")
    Call<ResponseBody> updateShoppingListItem(@Path("id") int ID, @Body ShoppingListItem item);

    @DELETE("/shoppinglist/{id}")
    Call<ResponseBody> deleteShoppingListItem(@Path("id") int ID);


    @GET("/user")
    Call<List<User>> getUsers();

    @GET("/user/{id}")
    Call<User> getUser(@Path("id") int ID);

    @POST("/user")
    Call<ResponseBody> addUser(@Body User user);

    @PUT("/user/{id}")
    Call<ResponseBody> updateUser(@Path("id") int ID, @Body User user);
}

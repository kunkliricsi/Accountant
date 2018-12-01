package com.kunkliricsi.accountant.database;

import android.content.ContentResolver;
import android.content.Context;

import com.kunkliricsi.accountant.database.local.AccountantDatabase;
import com.kunkliricsi.accountant.database.local.entities.Category;
import com.kunkliricsi.accountant.database.local.entities.Changes;
import com.kunkliricsi.accountant.database.local.entities.Expense;
import com.kunkliricsi.accountant.database.local.entities.Report;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;
import com.kunkliricsi.accountant.database.local.entities.User;
import com.kunkliricsi.accountant.database.local.synchelper.CategorySync;
import com.kunkliricsi.accountant.database.local.synchelper.ExpenseSync;
import com.kunkliricsi.accountant.database.local.synchelper.ReportSync;
import com.kunkliricsi.accountant.database.local.synchelper.ShoppingListItemSync;
import com.kunkliricsi.accountant.database.local.synchelper.SyncDatabase;
import com.kunkliricsi.accountant.database.local.synchelper.UserSync;
import com.kunkliricsi.accountant.database.network.NetworkManager;

import java.util.Date;
import java.util.List;

import androidx.room.Database;
import retrofit2.Call;
import retrofit2.Response;

public class DatabaseApi {

    private static DatabaseApi instance;

    private NetworkManager networkManager;
    private SyncDatabase syncDatabase;
    private AccountantDatabase database;

    private ContentResolver resolver;

    private User currentUser;
    private Report currentReport;

    public static DatabaseApi getInstance() throws Exception {
        if (instance == null) {
            throw new Exception("Database api has not yet been initialized.");
        }

        return instance;
    }

    private Syncer syncer;

    public interface Syncer {
        public void requestSync();
    }

    public static void Initialize(Context context, Syncer syncer, User user){
        if (instance == null) {
            instance = new DatabaseApi(context, syncer);

            if (user != null) {
                instance.setUser(user);
            }

            instance.updateCurrentReport();
        }
    }

    public void setUser(User user) {
        if (user != null){
            currentUser = user;
        }
        else {
            syncer.requestSync();

            // TODO: Change later
            currentUser = database.userDao().getUser(1);
        }
    }

    public void updateCurrentReport() {
        currentReport = this.getLastReport();
    }

    private DatabaseApi(Context context, Syncer syncer) {
        networkManager = NetworkManager.getInstance();
        syncDatabase = SyncDatabase.getInstance(context);
        database = AccountantDatabase.getInstance(context);

        this.syncer = syncer;
    }

    public Category getCategory(int id){
        syncer.requestSync();

        return database.categoryDao().getCategory(id);
    }

    public Category[] getCategories(){
        syncer.requestSync();

        return database.categoryDao().getAllCategories();
    }

    public String[] getCategoryNames(){
        syncer.requestSync();

        return database.categoryDao().getAllCategoryNames();
    }

    public Expense getExpense(int id){
        syncer.requestSync();

        return database.expenseDao().getExpense(id);
    }

    public Expense[] getReportExpenses(int id){
        syncer.requestSync();

        return database.expenseDao().getAllExpensesOfReport(id);
    }

    public Expense[] getCategoryExpenses(int id){
        syncer.requestSync();

        return database.expenseDao().getAllExpenseOfCategory(id);
    }

    public Expense[] getUserExpenses(int id){
        syncer.requestSync();

        return database.expenseDao().getAllExpensesOfUser(id);
    }

    public Expense[] getExpenses(){
        syncer.requestSync();

        return database.expenseDao().getAllExpenses();
    }

    public Report getReport(int id){
        syncer.requestSync();

        return database.reportDao().getReport(id);
    }

    public Report getLastReport(){
        syncer.requestSync();

        return database.reportDao().getLastReport();
    }

    public Report[] getEvaluatedReports(boolean evalueated){
        syncer.requestSync();

        return database.reportDao().getReportsByEvaluation(evalueated);
    }

    public Report[] getReports(){
        syncer.requestSync();

        return database.reportDao().getAllReports();
    }

    public ShoppingListItem getShoppingListItem(int id){
        syncer.requestSync();

        return database.shoppingListItemDao().getShoppingListItem(id);
    }

    public ShoppingListItem[] getShoppingListOfExpense(int id){
        syncer.requestSync();

        return database.shoppingListItemDao().getShoppingListOfExpense(id);
    }

    public ShoppingListItem[] getUnpaidShoppingList(){
        syncer.requestSync();

        return database.shoppingListItemDao().getAllUnpaidShoppingListItems();
    }

    public ShoppingListItem[] getShoppingList(){
        syncer.requestSync();

        return database.shoppingListItemDao().getShoppingList();
    }

    public User getUser(int id){
        syncer.requestSync();

        return  database.userDao().getUser(id);
    }

    public User[] getUsers(){
        syncer.requestSync();

        return database.userDao().getAllUsers();
    }

    public void addCategory(Category category){
        category.id = 0;
        database.categoryDao().addCategory(category);
        Category c = database.categoryDao().getLastCategory();
        CategorySync cs = new CategorySync();
        cs.id = 0;
        cs.post = c.id;
        syncDatabase.Dao().addCategory(cs);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.Category = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void deleteCategory(Category category){
        database.categoryDao().deleteCategory(category);
        CategorySync cs = new CategorySync();
        cs.id = 0;
        cs.delete = category.id;
        syncDatabase.Dao().addCategory(cs);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.Category = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void addExpense(Expense expense){
        expense.id = 0;
        expense.PurchaserID = currentUser.id;
        expense.ReportID = currentReport.id;
        database.expenseDao().addExpense(expense);
        Expense e = database.expenseDao().getLastExpense();
        ExpenseSync es = new ExpenseSync();
        es.id = 0;
        es.post = e.id;
        syncDatabase.Dao().addExpense(es);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.Expense = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void updateExpense(Expense expense){
        database.expenseDao().updateExpense(expense);
        ExpenseSync es = new ExpenseSync();
        es.id = 0;
        es.put = expense.id;
        syncDatabase.Dao().addExpense(es);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.Expense = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void addReport(Report report) {
        report.id = 0;
        database.reportDao().addReport(report);
        Report r = database.reportDao().getLastReport();
        ReportSync rs = new ReportSync();
        rs.id = 0;
        rs.post = r.id;
        syncDatabase.Dao().addReport(rs);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.Report = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void updateReport(Report report) {
        database.reportDao().updateReport(report);
        ReportSync rs = new ReportSync();
        rs.id = 0;
        rs.put = report.id;
        syncDatabase.Dao().addReport(rs);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.Report = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void addShoppingListItem(ShoppingListItem item){
        item.id = 0;
        database.shoppingListItemDao().addShoppingListItem(item);
        ShoppingListItem s = database.shoppingListItemDao().getLastShoppingListItem();
        ShoppingListItemSync ss = new ShoppingListItemSync();
        ss.id = 0;
        ss.post = s.id;
        syncDatabase.Dao().addShoppingListItem(ss);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.ShoppingListItem = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void updateShoppingListItem(ShoppingListItem item) {
        database.shoppingListItemDao().updateShoppingListItem(item);
        ShoppingListItemSync ss = new ShoppingListItemSync();
        ss.id = 0;
        ss.put = item.id;
        syncDatabase.Dao().addShoppingListItem(ss);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.ShoppingListItem = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void addUser(User user){
        user.id = 0;
        database.userDao().addUser(user);
        User u = database.userDao().getLastUser();
        UserSync us = new UserSync();
        us.id = 0;
        us.post = u.id;
        syncDatabase.Dao().addUser(us);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.User = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }

    public void updateUser(User user) {
        database.userDao().updateUser(user);
        UserSync us = new UserSync();
        us.id = 0;
        us.put = user.id;
        syncDatabase.Dao().addUser(us);

        Date date = new Date();
        Changes change = database.changesDao().getChanges();
        change.User = date;
        change.lastModified = date;
        database.changesDao().updateChanges(change);
    }
}

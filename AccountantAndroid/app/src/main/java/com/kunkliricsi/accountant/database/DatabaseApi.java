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

import java.util.Calendar;
import java.util.Date;
import java.util.List;

import android.arch.persistence.room.Database;
import android.os.StrictMode;

import retrofit2.Call;
import retrofit2.Response;

public class DatabaseApi {

    private static DatabaseApi instance;

    private static NetworkManager networkManager;
    private static SyncDatabase syncDatabase;
    private static AccountantDatabase database;

    private static User currentUser;
    private static Report currentReport;

    public static void Initialize(Context context, User user) throws Exception{
        if (instance == null) {
            instance = new DatabaseApi(context);

            instance.addInitialChanges();

            instance.setUser(user);
            instance.updateCurrentReport();
        }
    }

    private DatabaseApi(Context context) {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        networkManager = NetworkManager.getInstance();
        database = AccountantDatabase.getInstance(context);
        syncDatabase = SyncDatabase.getInstance(context);
    }

    public static void setUser(User user) throws Exception{
        if (user != null){
            currentUser = user;
        }
        else {
            try{
                Changes local = database.changesDao().getChanges();
                Changes server = networkManager.getChanges().execute().body();

                if (server.User.compareTo(local.User) != 0) {
                    syncUsers();
                    local.User = server.User;
                    database.changesDao().updateChanges(local);
                }

                // TODO: Change later
                currentUser = database.userDao().getUser(1);
            } catch (Exception ex) {
                throw new Exception("Cant set current user" + ex.getMessage());
            }
        }
    }

    public static void updateCurrentReport() throws Exception {
        try{
            networkManager.getChanges().execute();
            currentReport = getLastReport();
        } catch (Exception ex){
            throw new Exception("Cant set current report" + ex.getMessage());
        }
    }

    private void addInitialChanges(){
        if (database.changesDao().getChanges() != null)
            return;

        Changes change = new Changes();

        Calendar calendar = Calendar.getInstance();
        calendar.clear();

        calendar.set(Calendar.YEAR, 1996);
        calendar.set(Calendar.MONTH, 12);
        calendar.set(Calendar.DAY_OF_MONTH, 27);

        Date date = calendar.getTime();
        change.id = 1;
        change.User = date;
        change.ShoppingListItem = date;
        change.Category = date;
        change.Expense = date;
        change.Report = date;
        change.lastModified = date;

        database.changesDao().addChange(change);
    }

    public static Category getCategory(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Category.compareTo(local.Category) != 0) {
                syncCategories();
                local.Category = server.Category;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.categoryDao().getCategory(id);
    }

    public static Category[] getCategories(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Category.compareTo(local.Category) != 0) {
                syncCategories();
                local.Category = server.Category;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.categoryDao().getAllCategories();
    }

    public static String[] getCategoryNames(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Category.compareTo(local.Category) != 0) {
                syncCategories();
                local.Category = server.Category;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.categoryDao().getAllCategoryNames();
    }

    public static Expense getExpense(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Expense.compareTo(local.Expense) != 0) {
                syncExpenses();
                local.Expense = server.Expense;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.expenseDao().getExpense(id);
    }

    public static Expense[] getReportExpenses(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Expense.compareTo(local.Expense) != 0) {
                syncExpenses();
                local.Expense = server.Expense;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.expenseDao().getAllExpensesOfReport(id);
    }

    public static Expense[] getCategoryExpenses(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Expense.compareTo(local.Expense) != 0) {
                syncExpenses();
                local.Expense = server.Expense;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.expenseDao().getAllExpenseOfCategory(id);
    }

    public static Expense[] getUserExpenses(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Expense.compareTo(local.Expense) != 0) {
                syncExpenses();
                local.Expense = server.Expense;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.expenseDao().getAllExpensesOfUser(id);
    }

    public static Expense[] getExpenses(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Expense.compareTo(local.Expense) != 0) {
                syncExpenses();
                local.Expense = server.Expense;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.expenseDao().getAllExpenses();
    }

    public static Report getReport(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Report.compareTo(local.Report) != 0) {
                syncReports();
                local.Report = server.Report;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.reportDao().getReport(id);
    }

    public static Report getLastReport() {
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Report.compareTo(local.Report) != 0) {
                syncReports();
                local.Report = server.Report;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.reportDao().getLastReport();
    }

    public static Report[] getEvaluatedReports(boolean evaluated){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Report.compareTo(local.Report) != 0) {
                syncReports();
                local.Report = server.Report;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.reportDao().getReportsByEvaluation(evaluated);
    }

    public static Report[] getReports(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.Report.compareTo(local.Report) != 0) {
                syncReports();
                local.Report = server.Report;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){

        }

        return database.reportDao().getAllReports();
    }

    public static ShoppingListItem getShoppingListItem(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.ShoppingListItem.compareTo(local.ShoppingListItem) != 0) {
                syncShoppingList();
                local.ShoppingListItem = server.ShoppingListItem;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.shoppingListItemDao().getShoppingListItem(id);
    }

    public static ShoppingListItem[] getShoppingListOfExpense(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.ShoppingListItem.compareTo(local.ShoppingListItem) != 0) {
                syncShoppingList();
                local.ShoppingListItem = server.ShoppingListItem;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.shoppingListItemDao().getShoppingListOfExpense(id);
    }

    public static ShoppingListItem[] getUnpaidShoppingList(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.ShoppingListItem.compareTo(local.ShoppingListItem) != 0) {
                syncShoppingList();
                local.ShoppingListItem = server.ShoppingListItem;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.shoppingListItemDao().getAllUnpaidShoppingListItems();
    }

    public static String[] getAllUnpaidShoppingListItemNames() {

        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.ShoppingListItem.compareTo(local.ShoppingListItem) != 0) {
                syncShoppingList();
                local.ShoppingListItem = server.ShoppingListItem;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.shoppingListItemDao().getAllUnpaidShoppingListItemNames();
    }

    public static ShoppingListItem getShoppingListItemByName(String name) {
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.ShoppingListItem.compareTo(local.ShoppingListItem) != 0) {
                syncShoppingList();
                local.ShoppingListItem = server.ShoppingListItem;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.shoppingListItemDao().getShoppingListItemByName(name);
    }

    public static ShoppingListItem[] getShoppingList(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.ShoppingListItem.compareTo(local.ShoppingListItem) != 0) {
                syncShoppingList();
                local.ShoppingListItem = server.ShoppingListItem;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.shoppingListItemDao().getShoppingList();
    }

    public static User getUser(int id){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.User.compareTo(local.User) != 0) {
                syncUsers();
                local.User = server.User;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return  database.userDao().getUser(id);
    }

    public static User[] getUsers(){
        try{
            Changes local = database.changesDao().getChanges();
            Changes server = networkManager.getChanges().execute().body();

            if (server.User.compareTo(local.User) != 0) {
                syncUsers();
                local.User = server.User;
                database.changesDao().updateChanges(local);
            }

        }catch (Exception ex){
        }

        return database.userDao().getAllUsers();
    }

    public static void addCategory(Category category){
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

    public static void deleteCategory(Category category){
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

    public static void addExpense(Expense expense){
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

    public static void updateExpense(Expense expense){
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

    public static void addReport(Report report) {
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

    public static void updateReport(Report report) {
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

    public static void addShoppingListItem(ShoppingListItem item){
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

    public static void updateShoppingListItem(ShoppingListItem item) {
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

    public static void addUser(User user){
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

    public static void updateUser(User user) {
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

    public static void syncCategories() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostCategories();
        for (int id : postIds) {
            Category c = database.categoryDao().getCategory(id);
            c.id = 0;
            networkManager.addCategory(c).execute();
            syncDatabase.Dao().deletePostCategory(id);
        }

        int[] deleteIds = syncDatabase.Dao().getDeleteCategories();
        for (int id : deleteIds) {
            networkManager.deleteCategory(id);
            syncDatabase.Dao().deleteDeleteCategory(id);
        }

        database.categoryDao().deleteAll();
        List<Category> cs = networkManager.getCategories().execute().body();
        for (Category c : cs ) {
            database.categoryDao().addCategory(c);
        }
    }

    public static void syncExpenses() throws Exception{
        int[] postIds = syncDatabase.Dao().getPostExpenses();
        for (int id : postIds) {
            Expense e = database.expenseDao().getExpense(id);
            e.id = 0;
            networkManager.addExpense(e).execute();
            syncDatabase.Dao().deletePostExpense(id);
        }

        int[] putIds = syncDatabase.Dao().getPutExpenses();
        for (int id : putIds) {
            Expense e = database.expenseDao().getExpense(id);
            networkManager.updateExpense(e.id, e);
            syncDatabase.Dao().deletePutExpense(id);
        }

        database.expenseDao().deleteAll();
        List<Expense> es = networkManager.getExpenses().execute().body();
        for (Expense e : es) {
            database.expenseDao().addExpense(e);
        }
    }

    public static void syncReports() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostReports();
        for (int id : postIds) {
            Report r = database.reportDao().getReport(id);
            r.id = 0;
            networkManager.addReport(r);
            syncDatabase.Dao().deletePostReport(id);
        }

        int[] putIds = syncDatabase.Dao().getPutReports();
        for (int id : putIds) {
            Report r = database.reportDao().getReport(id);
            networkManager.updateReport(r.id, r);
            syncDatabase.Dao().deletePutReport(id);
        }

        database.reportDao().deleteAll();
        List<Report> rs = networkManager.getReports().execute().body();
        for (Report r : rs) {
            database.reportDao().addReport(r);
        }
    }

    public static void syncShoppingList() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostShoppingList();
        for (int id : postIds) {
            ShoppingListItem s = database.shoppingListItemDao().getShoppingListItem(id);
            s.id = 0;
            networkManager.addShoppingListItem(s);
            syncDatabase.Dao().deletePostShoppingListItem(id);
        }

        int[] putIds = syncDatabase.Dao().getPutShoppingList();
        for (int id : putIds) {
            ShoppingListItem s = database.shoppingListItemDao().getShoppingListItem(id);
            networkManager.updateShoppingListItem(s.id, s);
            syncDatabase.Dao().deletePutShoppingListItem(id);
        }

        database.shoppingListItemDao().deleteAll();
        List<ShoppingListItem> ss = networkManager.getShoppingList().execute().body();
        for (ShoppingListItem s : ss) {
            database.shoppingListItemDao().addShoppingListItem(s);
        }
    }

    public static void syncUsers() throws Exception {
        int[] postIds = syncDatabase.Dao().getPostUsers();
        for (int id : postIds) {
            User u = database.userDao().getUser(id);
            u.id = 0;
            networkManager.addUser(u);
            syncDatabase.Dao().deletePostUser(id);
        }

        int[] putIds = syncDatabase.Dao().getPutUsers();
        for (int id : putIds) {
            User u = database.userDao().getUser(id);
            networkManager.updateUser(u.id, u);
            syncDatabase.Dao().deletePutUser(id);
        }

        database.userDao().deleteAll();
        List<User> us = networkManager.getUsers().execute().body();
        for (User u : us) {
            database.userDao().addUser(u);
        }
    }
}

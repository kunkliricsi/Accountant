/*package com.kunkliricsi.accountant.local;

import android.content.ContentProvider;
import android.content.ContentValues;
import android.content.UriMatcher;
import android.database.Cursor;
import android.net.Uri;

import CategoryDao;
import ChangesDao;
import ExpenseDao;
import ReportDao;
import ShoppingListItemDao;
import UserDao;

import android.arch.persistence.room.Room;

public class AccountantContentProvider extends ContentProvider {

    private AccountantDatabase database;

    private CategoryDao categoryDao;
    private ChangesDao changesDao;
    private ExpenseDao expenseDao;
    private ReportDao reportDao;
    private ShoppingListItemDao shoppingListItemDao;
    private UserDao userDao;

    private static final String DBNAME = "Accountant";

    @Override
    public boolean onCreate() {

        database = Room.databaseBuilder(getContext(), AccountantDatabase.class, DBNAME).build();

        categoryDao = database.categoryDao();
        changesDao = database.changesDao();
        expenseDao = database.expenseDao();
        reportDao = database.reportDao();
        shoppingListItemDao = database.shoppingListItemDao();
        userDao = database.userDao();

        return true;
    }

    @Override
    public String getType(Uri uri) {
        return null;
    }

    private static final UriMatcher uriMatcher = new UriMatcher(UriMatcher.NO_MATCH);

    static {
        uriMatcher.addURI("com.example.app.provider", "categories", 1);
        uriMatcher.addURI("com.example.app.provider", "changes", 2);
        uriMatcher.addURI("com.example.app.provider", "expenses", 3);
        uriMatcher.addURI("com.example.app.provider", "reports", 4);
        uriMatcher.addURI("com.example.app.provider", "shoppinglist", 5);
        uriMatcher.addURI("com.example.app.provider", "users", 6);
    }

    public Cursor query(
            Uri uri,
            String[] projection,
            String selection,
            String[] selectionArgs,
            String sortOrder) {

        switch (uriMatcher.match(uri)) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                throw new IllegalArgumentException("Unknown URI: " + uri);
        }
    }

    @Override
    public int update(
            Uri uri,
            ContentValues values,
            String selection,
            String[] selectionArgs) {

        switch (uriMatcher.match(uri)) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                throw new IllegalArgumentException("Unknown URI: " + uri);
        }
    }

    @Override
    public Uri insert(Uri uri, ContentValues values) {

        switch (uriMatcher.match(uri)) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                throw new IllegalArgumentException("Unknown URI: " + uri);
        }
    }

    @Override
    public int delete(Uri uri, String selection, String[] selectionArgs) {

        switch (uriMatcher.match(uri)) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                throw new IllegalArgumentException("Unknown URI: " + uri);
        }

        return 0;
    }
}*/

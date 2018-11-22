package com.kunkliricsi.accountant.local;

import android.content.ContentProvider;
import android.content.ContentValues;
import android.content.UriMatcher;
import android.database.Cursor;
import android.net.Uri;
import android.text.TextUtils;

public class AccountantContentProvider extends ContentProvider {

    private AppDatabase appDatabase;

    @Override
    public boolean onCreate() {
        return true;
    }

    @Override
    public String getType(Uri uri) {
        return null;
    }

    private static final UriMatcher uriMatcher = new UriMatcher(UriMatcher.NO_MATCH);

    static {
        uriMatcher.addURI("com.example.app.provider", "category", 1);
        uriMatcher.addURI("com.example.app.provider", "changes", 2);
        uriMatcher.addURI("com.example.app.provider", "expense", 3);
        uriMatcher.addURI("com.example.app.provider", "report", 4);
        uriMatcher.addURI("com.example.app.provider", "shoppinglistitem", 5);
        uriMatcher.addURI("com.example.app.provider", "user", 6);
    }

    public Cursor query(
            Uri uri,
            String[] projection,
            String selection,
            String[] selectionArgs,
            String sortOrder) {

        switch (uriMatcher.match(uri)) {


            case 1:

                if (TextUtils.isEmpty(sortOrder)) sortOrder = "_ID ASC";
                break;

            case 2:

                selection = selection + "_ID = " + uri.getLastPathSegment();
                break;

            default:

        }
        // call the code to actually do the query
    }

    @Override
    public Uri insert(Uri uri, ContentValues values) {
        return null;
    }

    @Override
    public int delete(Uri uri, String selection, String[] selectionArgs) {
        return 0;
    }

    public int update(
            Uri uri,
            ContentValues values,
            String selection,
            String[] selectionArgs) {
        return 0;
    }
}

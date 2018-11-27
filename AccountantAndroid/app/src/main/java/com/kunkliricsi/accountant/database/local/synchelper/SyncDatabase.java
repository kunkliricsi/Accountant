package com.kunkliricsi.accountant.database.local.synchelper;

import android.content.Context;

import com.kunkliricsi.accountant.database.local.entities.User;

import androidx.room.Database;
import androidx.room.Room;
import androidx.room.RoomDatabase;

@Database(version = 1, entities = { CategorySync.class, ExpenseSync.class, ReportSync.class, ShoppingListItemSync.class, UserSync.class})
public abstract class SyncDatabase extends RoomDatabase {

    public abstract SyncDao Dao();

    private static SyncDatabase instance;

    public static SyncDatabase getInstance(Context context) {
        if (instance == null)
            instance = Room.databaseBuilder(context.getApplicationContext(), SyncDatabase.class, "Sync").build();

        return instance;
    }j
}

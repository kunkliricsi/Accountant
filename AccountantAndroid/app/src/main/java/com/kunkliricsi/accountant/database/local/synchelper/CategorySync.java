package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;

@Entity(tableName = "categorysync")
public class CategorySync {
    public int post;
    public int delete;
}

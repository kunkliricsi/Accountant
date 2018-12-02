package com.kunkliricsi.accountant.database.local.synchelper;

import android.arch.persistence.room.Entity;
import android.arch.persistence.room.PrimaryKey;

@Entity(tableName = "categorysync")
public class CategorySync {

    @PrimaryKey(autoGenerate = true)
    public int id;

    public int post;
    public int delete;
}

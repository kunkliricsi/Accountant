package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "reportsync")
public class ReportSync {

    @PrimaryKey(autoGenerate = true)
    public int id;

    public int post;
    public int put;
}

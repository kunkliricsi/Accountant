package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;

@Entity(tableName = "reportsync")
public class ReportSync {
    public int post;
    public int put;
}

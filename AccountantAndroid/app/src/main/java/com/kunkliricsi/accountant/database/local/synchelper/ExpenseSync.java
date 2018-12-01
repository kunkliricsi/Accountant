package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "expensesync")
public class ExpenseSync {

    @PrimaryKey(autoGenerate = true)
    public int id;

    public int post;
    public int put;
}

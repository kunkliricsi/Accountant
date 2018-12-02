package com.kunkliricsi.accountant.database.local.synchelper;

import android.arch.persistence.room.Entity;
import android.arch.persistence.room.PrimaryKey;

@Entity(tableName = "expensesync")
public class ExpenseSync {

    @PrimaryKey(autoGenerate = true)
    public int id;

    public int post;
    public int put;
}

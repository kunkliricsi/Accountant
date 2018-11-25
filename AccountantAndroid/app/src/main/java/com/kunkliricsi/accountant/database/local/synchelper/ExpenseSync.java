package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;

@Entity(tableName = "expensesync")
public class ExpenseSync {
    public int post;
    public int put;
}

package com.kunkliricsi.accountant.model.entities;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "changes")
public class Changes {

    @PrimaryKey
    public int id;

    @ColumnInfo(name = "category")
    public Date Category;

    @ColumnInfo(name = "expense")
    public Date Expense;

    @ColumnInfo(name = "report")
    public Date Report;

    @ColumnInfo(name = "shoppinglistitem")
    public Date ShoppingListItem;

    @ColumnInfo(name = "user")
    public Date User;
}

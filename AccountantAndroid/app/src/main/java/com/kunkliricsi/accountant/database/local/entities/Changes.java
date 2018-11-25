package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "changes")
public class Changes {

    @PrimaryKey
    public int id;

    @SerializedName("category")
    @ColumnInfo(name = "category")
    public Date Category;

    @SerializedName("expense")
    @ColumnInfo(name = "expense")
    public Date Expense;

    @SerializedName("report")
    @ColumnInfo(name = "report")
    public Date Report;

    @SerializedName("shoppingListItem")
    @ColumnInfo(name = "shoppinglistitem")
    public Date ShoppingListItem;

    @SerializedName("user")
    @ColumnInfo(name = "user")
    public Date User;

    @SerializedName("lastModified")
    @ColumnInfo(name = "lastmodified")
    public Date lastModified;
}

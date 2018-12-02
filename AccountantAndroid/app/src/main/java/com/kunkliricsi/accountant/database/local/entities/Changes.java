package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;
import com.kunkliricsi.accountant.database.local.utils.Converters;

import java.util.Date;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.PrimaryKey;
import android.arch.persistence.room.TypeConverters;

@Entity(tableName = "changes")
@TypeConverters(Converters.class)
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

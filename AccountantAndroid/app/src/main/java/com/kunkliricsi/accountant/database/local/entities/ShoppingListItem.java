package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;
import com.kunkliricsi.accountant.database.local.utils.Converters;

import java.util.Date;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.ForeignKey;
import android.arch.persistence.room.PrimaryKey;
import android.arch.persistence.room.TypeConverters;

@Entity(tableName = "shoppinglist")
@TypeConverters(Converters.class)
public class ShoppingListItem {

    @PrimaryKey(autoGenerate = true)
    public int id;

    @SerializedName("name")
    @ColumnInfo(name = "name")
    public String Name;

    @SerializedName("comment")
    @ColumnInfo(name = "comment")
    public String Comment;

    @SerializedName("dateOfCreation")
    @ColumnInfo(name = "dateofcreation")
    public Date DateOfCreation;

    @SerializedName("expenseID")
    @ColumnInfo(name = "expense")
    public int ExpenseID;
}

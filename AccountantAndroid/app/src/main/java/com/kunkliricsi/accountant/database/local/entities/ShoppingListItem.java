package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.ForeignKey;
import androidx.room.PrimaryKey;

@Entity(tableName = "shoppinglist",
        foreignKeys = @ForeignKey(
                            entity = Expense.class,
                            parentColumns = "id",
                            childColumns = "expense")
)
public class ShoppingListItem {

    @PrimaryKey
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

    @SerializedName("lastModified")
    @ColumnInfo(name = "lastmodified")
    public Date lastModified;
}

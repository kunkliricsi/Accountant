package com.kunkliricsi.accountant.model.entities;

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

    @ColumnInfo(name = "name")
    public String Name;

    @ColumnInfo(name = "comment")
    public String Comment;

    @ColumnInfo(name = "dateofcreation")
    public Date DateOfCreation;

    @ColumnInfo(name = "expense")
    public int ExpenseID;
}

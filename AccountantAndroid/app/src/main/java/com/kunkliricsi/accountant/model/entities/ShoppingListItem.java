package com.kunkliricsi.accountant.model.entities;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity
public class ShoppingListItem {

    @PrimaryKey
    public int ID;

    @ColumnInfo(name = "name")
    public String Name;

    @ColumnInfo(name = "comment")
    public String Comment;

    @ColumnInfo(name = "dateofcreation")
    public Date DateOfCreation;

    @ColumnInfo(name = "expense")
    public int ExpenseID;
}

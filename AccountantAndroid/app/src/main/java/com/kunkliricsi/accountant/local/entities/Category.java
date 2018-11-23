package com.kunkliricsi.accountant.local.entities;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "categories")
public class Category {

    @PrimaryKey
    public int id;

    @ColumnInfo(name = "name")
    public String Name;

    @ColumnInfo(name = "description")
    public String Description;
}

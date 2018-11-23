package com.kunkliricsi.accountant.local.entities;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "users")
public class User {

    @PrimaryKey
    public int id;

    @ColumnInfo(name = "name")
    public String Name;

    @ColumnInfo(name = "email")
    public String Email;
}

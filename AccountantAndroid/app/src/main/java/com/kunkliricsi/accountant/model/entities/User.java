package com.kunkliricsi.accountant.model.entities;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity
public class User {

    @PrimaryKey
    public int ID;

    @ColumnInfo(name = "name")
    public String Name;

    @ColumnInfo(name = "email")
    public String Email;
}

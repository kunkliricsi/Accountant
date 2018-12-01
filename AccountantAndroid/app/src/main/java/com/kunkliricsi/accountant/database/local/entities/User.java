package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "users")
public class User {

    @PrimaryKey(autoGenerate = true)
    public int id;

    @SerializedName("name")
    @ColumnInfo(name = "name")
    public String Name;

    @SerializedName("email")
    @ColumnInfo(name = "email")
    public String Email;
}

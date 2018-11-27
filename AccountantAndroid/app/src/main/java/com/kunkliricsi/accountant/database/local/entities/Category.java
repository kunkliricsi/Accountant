package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "categories")
public class Category {

    @PrimaryKey
    public int id;

    @SerializedName("name")
    @ColumnInfo(name = "name")
    public String Name;

    @SerializedName("description")
    @ColumnInfo(name = "description")
    public String Description;
}

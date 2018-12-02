package com.kunkliricsi.accountant.database.local.entities;

import android.arch.persistence.room.Entity;

import com.google.gson.annotations.SerializedName;
import com.kunkliricsi.accountant.database.local.utils.Converters;

import java.util.Date;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.PrimaryKey;
import android.arch.persistence.room.TypeConverters;

@Entity(tableName = "categories")
public class Category {

    @PrimaryKey(autoGenerate = true)
    public int id;

    @SerializedName("name")
    @ColumnInfo(name = "name")
    public String Name;

    @SerializedName("description")
    @ColumnInfo(name = "description")
    public String Description;
}

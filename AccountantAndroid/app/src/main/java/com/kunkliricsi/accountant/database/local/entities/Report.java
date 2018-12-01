package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "reports")
public class Report {

    @PrimaryKey(autoGenerate = true)
    public int id;

    @SerializedName("start")
    @ColumnInfo(name = "start")
    public Date Start;

    @SerializedName("end")
    @ColumnInfo(name = "end")
    public Date End;

    @SerializedName("evaluated")
    @ColumnInfo(name = "evaluated")
    public boolean Evaluated;

    @SerializedName("dateOfEvaluation")
    @ColumnInfo(name = "dateofevaluation")
    public Date DateOfEvaluation;
}

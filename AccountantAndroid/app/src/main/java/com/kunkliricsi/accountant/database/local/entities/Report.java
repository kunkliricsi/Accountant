package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;
import com.kunkliricsi.accountant.database.local.utils.Converters;

import java.util.Date;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.PrimaryKey;
import android.arch.persistence.room.TypeConverters;

@Entity(tableName = "reports")
@TypeConverters(Converters.class)
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

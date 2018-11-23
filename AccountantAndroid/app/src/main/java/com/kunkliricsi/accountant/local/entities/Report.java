package com.kunkliricsi.accountant.local.entities;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "reports")
public class Report {

    @PrimaryKey
    public int id;

    @ColumnInfo(name = "start")
    public Date Start;

    @ColumnInfo(name = "end")
    public Date End;

    @ColumnInfo(name = "evaluated")
    public boolean Evaluated;

    @ColumnInfo(name = "dateofevaluation")
    public Date DateOfEvaluation;
}

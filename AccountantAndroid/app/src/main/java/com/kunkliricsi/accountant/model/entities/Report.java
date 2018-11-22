package com.kunkliricsi.accountant.model.entities;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity
public class Report {

    @PrimaryKey
    public int ID;

    @ColumnInfo(name = "start")
    public Date Start;

    @ColumnInfo(name = "end")
    public Date End;

    @ColumnInfo(name = "evaluated")
    public boolean Evaluated;

    @ColumnInfo(name = "dateofevaluation")
    public Date DateOfEvaluation;
}

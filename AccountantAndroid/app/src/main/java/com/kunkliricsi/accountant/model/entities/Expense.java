package com.kunkliricsi.accountant.model.entities;

import com.kunkliricsi.accountant.model.entities.enums.PayOption;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity
public class Expense {

    @PrimaryKey
    public int ID;

    @ColumnInfo(name = "amount")
    public int Amount;

    @ColumnInfo(name = "payoption")
    public PayOption PayOption;

    @ColumnInfo(name = "dateofpurchase")
    public Date DateOfPurchase;

    @ColumnInfo(name = "report")
    public int ReportID;

    @ColumnInfo(name = "category")
    public int CategoryID;

    @ColumnInfo(name = "purchaser")
    public int PurchaserID;
}

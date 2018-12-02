package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;
import com.kunkliricsi.accountant.database.local.utils.Converters;

import java.util.Date;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.ForeignKey;
import android.arch.persistence.room.PrimaryKey;
import android.arch.persistence.room.TypeConverters;

@Entity(tableName = "expenses")
@TypeConverters(Converters.class)
public class Expense {

    @PrimaryKey(autoGenerate = true)
    public int id;

    @SerializedName("amount")
    @ColumnInfo(name = "amount")
    public int Amount;

    @SerializedName("payOptions")
    @ColumnInfo(name = "payoption")
    public int PayOption;

    @SerializedName("dateOfPurchase")
    @ColumnInfo(name = "dateofpurchase")
    public Date DateOfPurchase;

    @SerializedName("reportID")
    @ColumnInfo(name = "report")
    public int ReportID;

    @SerializedName("categoryID")
    @ColumnInfo(name = "category")
    public int CategoryID;

    @SerializedName("purchaserID")
    @ColumnInfo(name = "purchaser")
    public int PurchaserID;
}

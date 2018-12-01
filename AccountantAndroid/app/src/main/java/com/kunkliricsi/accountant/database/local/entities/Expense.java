package com.kunkliricsi.accountant.database.local.entities;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.ForeignKey;
import androidx.room.PrimaryKey;

@Entity(tableName = "expenses",
        foreignKeys = {
        @ForeignKey(
                entity = Report.class,
                parentColumns = "id",
                childColumns = "report"
        ),
        @ForeignKey(
                entity = Category.class,
                parentColumns = "id",
                childColumns = "category"
        ),
        @ForeignKey(
                entity = User.class,
                parentColumns = "id",
                childColumns = "purchaser"
        ),
})
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

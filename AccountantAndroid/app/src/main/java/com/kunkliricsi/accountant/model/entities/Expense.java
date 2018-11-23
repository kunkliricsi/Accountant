package com.kunkliricsi.accountant.model.entities;

import com.kunkliricsi.accountant.model.entities.enums.PayOption;

import java.io.PushbackReader;
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

    @PrimaryKey
    public int id;

    @ColumnInfo(name = "amount")
    public int Amount;

    @ColumnInfo(name = "payoption")
    public int PayOption;

    @ColumnInfo(name = "dateofpurchase")
    public Date DateOfPurchase;

    @ColumnInfo(name = "report")
    public int ReportID;

    @ColumnInfo(name = "category")
    public int CategoryID;

    @ColumnInfo(name = "purchaser")
    public int PurchaserID;
}

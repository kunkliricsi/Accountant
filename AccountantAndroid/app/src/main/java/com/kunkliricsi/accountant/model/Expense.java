package com.kunkliricsi.accountant.model;

import com.kunkliricsi.accountant.model.enums.PayOption;

import java.util.Date;
import java.util.List;

public class Expense {
    public int ID;
    public User Purchaser;
    public int Amount;
    public Category Category;
    public List<ShoppingListItem> ItemsPurchased;
    public PayOption PayOption;
    public Date DateOfPurchase;
    public Report Report;
}

package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;

@Entity(tableName = "shoppinglistsync")
public class ShoppingListItemSync {
    public int post;
    public int put;
}

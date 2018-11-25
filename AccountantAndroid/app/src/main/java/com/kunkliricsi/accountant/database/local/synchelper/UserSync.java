package com.kunkliricsi.accountant.database.local.synchelper;

import androidx.room.Entity;

@Entity(tableName = "usersync")
public class UserSync {
    public int post;
    public int put;
}

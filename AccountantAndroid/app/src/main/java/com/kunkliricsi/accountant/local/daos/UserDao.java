package com.kunkliricsi.accountant.local.daos;

import com.kunkliricsi.accountant.local.entities.User;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

@Dao
public interface UserDao {

    @Query("SELECT * FROM users")
    public User[] getAllUsers();

    @Query("SELECT * FROM users WHERE id = :id")
    public User getUser(int id);

    @Update
    public void updateUser(User... users);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addUser(User... users);
}

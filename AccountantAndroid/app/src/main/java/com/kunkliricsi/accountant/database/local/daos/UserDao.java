package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.User;

import androidx.room.Dao;
import androidx.room.Delete;
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

    @Query("SELECT * FROM users ORDER BY id DESC LIMIT 1")
    public User getLastUser();

    @Update
    public void updateUser(User... users);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addUser(User... users);

    @Delete
    public void deleteUser(User... users);

    @Query("DELETE FROM users")
    public void deleteAll();
}

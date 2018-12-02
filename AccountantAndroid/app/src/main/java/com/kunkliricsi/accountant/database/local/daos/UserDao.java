package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.User;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.OnConflictStrategy;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

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

package com.kunkliricsi.accountant.local.daos;

import com.kunkliricsi.accountant.local.entities.Changes;

import androidx.room.Dao;
import androidx.room.Query;
import androidx.room.Update;

@Dao
public interface ChangesDao {

    @Query("SELECT * FROM changes")
    public Changes getChanges();

    @Update
    public void updateChanges(Changes change);
}

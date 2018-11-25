package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Changes;

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

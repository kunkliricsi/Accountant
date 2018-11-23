package com.kunkliricsi.accountant.model.daos;

import com.kunkliricsi.accountant.model.entities.Changes;

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

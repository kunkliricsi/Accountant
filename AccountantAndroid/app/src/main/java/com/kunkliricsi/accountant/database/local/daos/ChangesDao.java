package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Changes;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

@Dao
public interface ChangesDao {

    @Query("SELECT * FROM changes")
    public Changes getChanges();

    @Update
    public void updateChanges(Changes change);

    @Insert
    public void addChange(Changes... change);
}

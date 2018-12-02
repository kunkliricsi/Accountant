package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Report;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.OnConflictStrategy;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

@Dao
public interface ReportDao {

    @Query("SELECT * FROM reports")
    public Report[] getAllReports();

    @Query("SELECT * FROM reports WHERE id = :id")
    public Report getReport(int id);

    @Query("SELECT * FROM reports ORDER BY id DESC LIMIT 1")
    public Report getLastReport();

    @Query("SELECT * FROM reports WHERE evaluated = :evaluated")
    public Report[] getReportsByEvaluation(boolean evaluated);

    @Update
    public void updateReport(Report... reports);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addReport(Report... reports);

    @Delete
    public void deleteReport(Report... reports);

    @Query("DELETE FROM reports")
    public void deleteAll();
}

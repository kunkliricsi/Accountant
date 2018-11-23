package com.kunkliricsi.accountant.model.daos;

import com.kunkliricsi.accountant.model.entities.Report;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

@Dao
public interface ReportDao {

    @Query("SELECT * FROM reports")
    public Report[] getAllReports();

    @Query("SELECT * FROM reports WHERE id = :id")
    public Report getReport(int id);

    @Query("SELECT * FROM reports WHERE evaluated = :evaluated")
    public Report[] getReportsByEvaluation(boolean evaluated);

    @Update
    public void updateReport(Report... reports);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addReport(Report... reports);
}

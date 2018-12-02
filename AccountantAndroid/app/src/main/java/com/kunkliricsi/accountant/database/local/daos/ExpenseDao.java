package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Expense;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.OnConflictStrategy;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

@Dao
public interface ExpenseDao {

    @Query("SELECT * FROM expenses")
    public Expense[] getAllExpenses();

    @Query("SELECT * FROM expenses WHERE id = :id")
    public Expense getExpense(int id);

    @Query("SELECT * FROM expenses ORDER BY id DESC LIMIT 1")
    public Expense getLastExpense();

    @Query("SELECT * FROM expenses WHERE report = :id")
    public Expense[] getAllExpensesOfReport(int id);

    @Query("SELECT * FROM expenses WHERE category = :id")
    public Expense[] getAllExpenseOfCategory(int id);

    @Query("SELECT * FROM expenses WHERE purchaser = :id")
    public Expense[] getAllExpensesOfUser(int id);

    @Update
    public void updateExpense(Expense... expenses);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addExpense(Expense... expenses);

    @Delete
    public void deleteExpense(Expense... expenses);

    @Query("DELETE FROM expenses")
    public void deleteAll();
}

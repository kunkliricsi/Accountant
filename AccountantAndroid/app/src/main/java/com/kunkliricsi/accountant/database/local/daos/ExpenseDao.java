package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Expense;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

@Dao
public interface ExpenseDao {

    @Query("SELECT * FROM expenses")
    public Expense[] getAllExpenses();

    @Query("SELECT * FROM expenses WHERE id = :id")
    public Expense getExpense(int id);

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
}

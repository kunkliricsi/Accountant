package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Category;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;

@Dao
public interface CategoryDao {

    @Query("SELECT * FROM categories")
    public Category[] getAllCategories();

    @Query("SELECT * FROM categories WHERE id = :id")
    public Category getCategory(int id);

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addCategory(Category... categories);

    @Delete
    public void deleteCategory(Category... categories);

    @Query("DELETE FROM categories")
    public void deleteAll();
}

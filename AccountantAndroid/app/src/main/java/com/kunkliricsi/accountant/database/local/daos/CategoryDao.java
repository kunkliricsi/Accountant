package com.kunkliricsi.accountant.database.local.daos;

import com.kunkliricsi.accountant.database.local.entities.Category;

import java.util.List;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.OnConflictStrategy;
import android.arch.persistence.room.Query;

@Dao
public interface CategoryDao {

    @Query("SELECT * FROM categories")
    public Category[] getAllCategories();

    @Query("SELECT name FROM categories")
    public String[] getAllCategoryNames();

    @Query("SELECT * FROM categories WHERE id = :id")
    public Category getCategory(int id);

    @Query("SELECT * FROM categories ORDER BY id DESC LIMIT 1")
    public Category getLastCategory();

    @Insert(onConflict = OnConflictStrategy.FAIL)
    public void addCategory(Category... categories);

    @Delete
    public void deleteCategory(Category... categories);

    @Query("DELETE FROM categories")
    public void deleteAll();
}

using Accountant.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Accountant.DAL
{
    public class AccountantContext : DbContext
    {
        public AccountantContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountantContext).Assembly);

            SeedDatabase(modelBuilder);
        }

        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "testCategory", Description = "testDescription" });

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "testGroup" });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "testUser",
                    Email = "test@email.com",
                    PasswordHash = new byte[] { },
                    PasswordSalt = new byte[] { }
                });

            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup { UserId = 1, GroupId = 1 });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 1,
                    GroupId = 1,
                    StartDate = new DateTime(1996, 12, 12),
                    EndDate = new DateTime(1997, 1, 12),
                    IsEvaluated = false,
                    EvaluationDate = null
                });

            modelBuilder.Entity<Expense>().HasData(
                new Expense
                {
                    Id = 1,
                    ReportId = 1,
                    UserId = 1,
                    Amount = 1545,
                    CategoryId = 1,
                    PurchaseDate = new DateTime(1996, 12, 27)
                });

            modelBuilder.Entity<ShoppingList>().HasData(
                new ShoppingList { Id = 1, Name = "testList", GroupId = 1 });

            modelBuilder.Entity<ShoppingListItem>().HasData(
                new ShoppingListItem { Id = 1, Name = "testItem1", IsTicked = false, ShoppingListId = 1 },
                new ShoppingListItem { Id = 2, Name = "testItem2", IsTicked = true, ShoppingListId = 1 });
        }
    }
}

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
                new Category { Id = 1, Name = "seedCategory1", Description = "seedDescription1" },
                new Category { Id = 2, Name = "seedCategory2", Description = "seedDescription2" });

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "seedGroup1" },
                new Group { Id = 2, Name = "seedGroup2" });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "seedUser1",
                    Email = "seed1@email.com",
                    PasswordHash = new byte[] { },
                    PasswordSalt = new byte[] { }
                },
                new User
                {
                    Id = 2,
                    Name = "seedUser2",
                    Email = "seed2@email.com",
                    PasswordHash = new byte[] { },
                    PasswordSalt = new byte[] { }
                });

            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup { UserId = 1, GroupId = 1 },
                new UserGroup { UserId = 1, GroupId = 2 },
                new UserGroup { UserId = 2, GroupId = 1 });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 1,
                    GroupId = 1,
                    StartDate = new DateTime(1996, 12, 12),
                    EndDate = new DateTime(1997, 1, 12),
                    IsEvaluated = true,
                    EvaluationDate = new DateTime(1997, 1, 13)
                },
                new Report
                {
                    Id = 2,
                    GroupId = 1,
                    StartDate = new DateTime(1997, 1, 12),
                    EndDate = new DateTime(1997, 2, 12),
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
                },
                new Expense
                {
                    Id = 2,
                    ReportId = 1,
                    UserId = 1,
                    Amount = 15800,
                    CategoryId = 2,
                    PurchaseDate = new DateTime(1997, 1, 1)
                });

            modelBuilder.Entity<ShoppingList>().HasData(
                new ShoppingList { Id = 1, Name = "seedList1", GroupId = 2 },
                new ShoppingList { Id = 2, Name = "seedList2", GroupId = 2 });

            modelBuilder.Entity<ShoppingListItem>().HasData(
                new ShoppingListItem { Id = 1, Name = "seedItem1", IsTicked = false, ShoppingListId = 1 },
                new ShoppingListItem { Id = 2, Name = "seedItem2", IsTicked = true, ShoppingListId = 1 });
        }
    }
}

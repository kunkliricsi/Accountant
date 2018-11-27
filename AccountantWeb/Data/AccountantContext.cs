using System;
using System.Linq;
using Accountant.Models;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Data
{
    public class AccountantContext : DbContext
    {
        public AccountantContext(DbContextOptions<AccountantContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Changes> Changes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingListItem> ShoppingList { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Changes>().ToTable("Changes")
                .Property(c => c.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().ToTable("User")
                .Property(u => u.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Report>().ToTable("Report")
                .Property(r => r.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>().ToTable("Category")
                .Property(c => c.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItem")
                .Property(s => s.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Expense>().ToTable("Expense")
                .Property(e => e.ID)
                .ValueGeneratedOnAdd();
        }

        public int FirstSave()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges()
        {
            var changeEntity = Changes.Find(1);

            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();
            
            var now = DateTime.UtcNow;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Kind);

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                this.SetChange(ref changeEntity, entityName, now);
            }
            
            changeEntity.lastModified = now;
            Changes.Update(changeEntity);
            return base.SaveChanges();
        }

        private void SetChange(ref Changes change, string name, DateTime now)
        {
            typeof(Changes).GetProperty(name).SetValue(change, now, null);
        }
    }
}
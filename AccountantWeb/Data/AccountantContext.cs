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
            modelBuilder.Entity<Changes>().ToTable("Changes");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItem");
            modelBuilder.Entity<Expense>().ToTable("Expense");
        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                this.SetChange(entityName);
            }

            return base.SaveChanges();
        }

        private void SetChange(string name)
        {
            var now = DateTime.UtcNow;
            var change = Changes.Find(1);

            typeof(Changes).GetProperty(name).SetValue(change, now, null);
            Changes.Update(change);
        }
    }
}
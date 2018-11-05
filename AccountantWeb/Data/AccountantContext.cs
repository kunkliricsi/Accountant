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

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingListItem> ShoppingList { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItem");
            modelBuilder.Entity<Report>().ToTable("Report");

            modelBuilder.Entity<Expense>().ToTable("Expense")
                .HasOne<Report>(e => e.Report)
                .WithMany(r => r.Expenses)
                .HasForeignKey(e => e.ReportID);
        }
    }
}
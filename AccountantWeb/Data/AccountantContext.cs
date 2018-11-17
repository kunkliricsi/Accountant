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
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItem")
                .HasOne<Expense>(i => i.Expense)
                .WithMany(e => e.ItemsPurchased);

            modelBuilder.Entity<Expense>().ToTable("Expense")
                .HasOne<Report>(e => e.Report)
                .WithMany(r => r.Expenses)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .HasOne<User>(e => e.Purchaser)
                .WithMany(u => u.Expenses)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .HasOne<Category>(e => e.Category)
                .WithMany(c => c.Expenses);
        }
    }
}
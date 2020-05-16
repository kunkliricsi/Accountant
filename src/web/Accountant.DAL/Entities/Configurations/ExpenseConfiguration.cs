using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountant.DAL.Entities.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(e => e.Amount)
                .IsRequired();

            builder.Property(e => e.PurchaseDate)
                .IsRequired();

            builder.HasOne(e => e.Report)
                .WithMany(r => r.Expenses)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Category)
                .WithMany();

            builder.HasOne(e => e.User)
                .WithMany()
                .IsRequired();
        }
    }
}

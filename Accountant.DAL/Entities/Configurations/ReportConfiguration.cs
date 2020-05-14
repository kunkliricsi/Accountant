using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountant.DAL.Entities.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired();

            builder.Property(r => r.IsEvaluated)
                .HasDefaultValue(false);

            builder.HasMany(r => r.Expenses)
                .WithOne(e => e.Report)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

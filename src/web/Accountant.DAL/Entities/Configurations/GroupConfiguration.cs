using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountant.DAL.Entities.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name)
                .IsRequired();

            builder.HasMany(g => g.Reports)
                .WithOne(r => r.Group)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.ShoppingLists)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

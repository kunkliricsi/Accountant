using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountant.DAL.Entities.Configurations
{
    public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.Property(sl => sl.Name)
                .IsRequired();

            builder.HasMany(sl => sl.ShoppingListItems)
                .WithOne(sli => sli.ShoppingList)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

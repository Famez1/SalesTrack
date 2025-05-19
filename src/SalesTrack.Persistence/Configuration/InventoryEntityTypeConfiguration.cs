using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesTrack.Domain.Entities;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantity)
            .IsRequired();

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Inventories)
            .HasForeignKey(i => i.ProductId);
    }
}

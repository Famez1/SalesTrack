using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnType("decimal(18,2)");
    }
}

using Absplan.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Abstractions.Persistence.Base;
using SalesTrack.Domain.Entities;
using System.Reflection;

namespace SalesTrack.Persistence;

public class SalesTrackDbContext : BaseDbContext, ISalesTrackDbContext
{
    public SalesTrackDbContext(DbContextOptions<SalesTrackDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Sale> Sales { get; set; }

    public DbSet<SaleItem> SaleItems { get; set; }

    protected override void ConfigureModelBuilder(ModelBuilder modelBuilder)
    {
        base.ConfigureModelBuilder(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyConfiguration(new AuditableEntityTypeConfiguration<Category>());
        modelBuilder.ApplyConfiguration(new AuditableEntityTypeConfiguration<Inventory>());
        modelBuilder.ApplyConfiguration(new AuditableEntityTypeConfiguration<Product>());
        modelBuilder.ApplyConfiguration(new AuditableEntityTypeConfiguration<Sale>());
        modelBuilder.ApplyConfiguration(new AuditableEntityTypeConfiguration<SaleItem>());
    }
}

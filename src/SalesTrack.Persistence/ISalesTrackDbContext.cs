using Microsoft.EntityFrameworkCore;
using SalesTrack.Abstractions.Persistence.Interfaces;
using SalesTrack.Domain.Entities;

namespace SalesTrack.Persistence;

public interface ISalesTrackDbContext : IDbContext
{
    public DbSet<User> User { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Sale> Sales { get; set; }

    public DbSet<SaleItem> SaleItems { get; set; }
}

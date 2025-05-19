using SalesTrack.Abstractions.Persistence.Interfaces;

namespace SalesTrack.Domain.Entities;

public class Product : IAuditableEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } 

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Category Category { get; set; } = default!;

    public List<Inventory> Inventories { get; set; } = [];

    public List<SaleItem> SaleItems { get; set; } = [];
}

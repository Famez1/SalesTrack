using SalesTrack.Abstractions.Persistence.Interfaces;

namespace SalesTrack.Domain.Entities;

public class Inventory : IAuditableEntity
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Product Product { get; set; }
}

using SalesTrack.Abstractions.Persistence.Interfaces;
using SalesTrack.Domain.Entities;

public class Category : IAuditableEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } 

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<Product> Products { get; set; } = [];
}

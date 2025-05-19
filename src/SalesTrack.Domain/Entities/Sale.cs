using SalesTrack.Abstractions.Persistence.Interfaces;
using SalesTrack.Domain.Entities;

public class Sale : IAuditableEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid? CustomerId { get; set; } 

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<SaleItem> SaleItems { get; set; } = [];
}

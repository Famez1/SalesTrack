using SalesTrack.Abstractions.Persistence.Interfaces;
using SalesTrack.Domain.Entities;

public class Sale : IAuditableEntity
{
    /// <summary>
    /// Уникальный ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата продажи
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Сумма продажи
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления записи
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public List<SaleItem> SaleItems { get; set; } = [];
}

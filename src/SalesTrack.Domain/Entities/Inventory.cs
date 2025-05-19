using SalesTrack.Abstractions.Persistence.Interfaces;

namespace SalesTrack.Domain.Entities;

/// <summary>
/// Остатки на складе
/// </summary>
public class Inventory : IAuditableEntity
{
    /// <summary>
    /// Уникальный ID записи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Товар
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Количество на складе
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления записи
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public Product Product { get; set; }
}

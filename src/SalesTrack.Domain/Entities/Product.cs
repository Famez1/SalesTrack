using SalesTrack.Abstractions.Persistence.Interfaces;

namespace SalesTrack.Domain.Entities;

/// <summary>
/// Товар
/// </summary>
public class Product : IAuditableEntity
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название товара
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Категория товара
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Текущая цена
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления записи
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public Category Category { get; set; } = default!;

    public List<Inventory> Inventories { get; set; } = [];

    public List<SaleItem> SaleItems { get; set; } = [];
}

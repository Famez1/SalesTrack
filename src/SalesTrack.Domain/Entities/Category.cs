using SalesTrack.Abstractions.Persistence.Interfaces;
using SalesTrack.Domain.Entities;

/// <summary>
/// Категория товара
/// </summary>
public class Category : IAuditableEntity
{
    /// <summary>
    /// Уникальный ID категории
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления записи
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public List<Product> Products { get; set; } = [];
}

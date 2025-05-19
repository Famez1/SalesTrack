using SalesTrack.Abstractions.Persistence.Interfaces;

namespace SalesTrack.Domain.Entities;

/// <summary>
/// Проданные товары
/// </summary>
public class SaleItem : IAuditableEntity
{
    /// <summary>
    /// Уникальный ID записи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Продажа
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Товар
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Кол-во проданных единиц
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена за единицу на момент продажи
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

    public Sale Sale { get; set; } 

    public Product Product { get; set; } 
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Domain.Entities;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Sales.Commands.AddSale;

public class AddSaleCommandHandler(ISalesTrackDbContext salesTrackDbContext) : IRequestHandler<AddSaleCommand>
{
    public async Task Handle(
        AddSaleCommand command, 
        CancellationToken cancellationToken)
    {
        var productIds = command.SaledProducts.Select(x => x.ProductId).ToList();

        var allProducts = await salesTrackDbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var productPriceInfo = command.SaledProducts
           .Join(allProducts,
               cmdProduct => cmdProduct.ProductId,
               dbProduct => dbProduct.Id,
               (cmdProduct, dbProduct) => new
               {
                   ProductId = cmdProduct.ProductId,
                   Quantity = cmdProduct.Quantity,
                   Price = dbProduct.Price
               })
           .ToDictionary(x => x.ProductId, x => (x.Quantity, x.Price));

        var inventories = salesTrackDbContext.Inventories
            .Where(x => productIds.Contains(x.ProductId))
            .ToList();

        decimal totalAmount = (productPriceInfo.Sum(p => p.Value.Quantity * p.Value.Price)); ;

        foreach (var inventory in inventories)
        {
            if (productPriceInfo.TryGetValue(inventory.ProductId, out var saleInfo))
            {
                inventory.Quantity -= saleInfo.Quantity;
            }
        }

        var sale = new Sale
        {
            Date = DateTime.UtcNow.Date,
            TotalAmount = totalAmount,
            SaleItems = command.SaledProducts
            .Select(product => new SaleItem 
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                Price = productPriceInfo[product.ProductId].Price,
            })
            .ToList()
        };

        salesTrackDbContext.Sales.Add(sale);

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }
}

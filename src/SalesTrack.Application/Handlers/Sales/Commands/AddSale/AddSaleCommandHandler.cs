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
        var productNames = command.SaledProducts.Select(x => x.ProductName).ToList();

        var allProducts = await salesTrackDbContext.Products
            .Where(p => productNames.Contains(p.Name))
            .ToListAsync(cancellationToken);

        var productPriceInfo = command.SaledProducts
           .Join(allProducts,
               cmdProduct => cmdProduct.ProductName,
               dbProduct => dbProduct.Name,
               (cmdProduct, dbProduct) => new
               {
                   ProductId = cmdProduct.ProductName,
                   Quantity = cmdProduct.Quantity,
                   Price = dbProduct.Price
               })
           .ToDictionary(x => x.ProductId, x => (x.Quantity, x.Price));

        var inventories = salesTrackDbContext.Inventories
            .Include(x => x.Product)
            .Where(x => productNames.Contains(x.Product.Name))
            .ToList();

        decimal totalAmount = (productPriceInfo.Sum(p => p.Value.Quantity * p.Value.Price)); ;

        foreach (var inventory in inventories)
        {
            if (productPriceInfo.TryGetValue(inventory.Product.Name, out var saleInfo))
            {
                inventory.Quantity -= saleInfo.Quantity;
            }
        }

        var productDictByName = allProducts.ToDictionary(p => p.Name, p => p);

        var sale = new Sale
        {
            Date = DateTime.UtcNow.Date,
            TotalAmount = totalAmount,
            SaleItems = command.SaledProducts
                .Where(product => productDictByName.ContainsKey(product.ProductName))
                .Select(product => {
                    var dbProduct = productDictByName[product.ProductName];
                    return new SaleItem
                    {
                        ProductId = dbProduct.Id,
                        Quantity = product.Quantity,
                        Price = dbProduct.Price
                    };
                })
                .ToList()
        };

        salesTrackDbContext.Sales.Add(sale);

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }
}

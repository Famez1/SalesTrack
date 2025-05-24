using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Common.Models;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Sales.Queries.GetSales;

public class GetSalesQueryHandler(
    ISalesTrackDbContext salesTrackDbContext,
    IMapper mapper) : IRequestHandler<GetSalesQuery, List<GetSalesQueryResult>>
{
    public async Task<List<GetSalesQueryResult>> Handle(
        GetSalesQuery query,
        CancellationToken cancellationToken)
    {
        query.Limit ??= 20;
        query.Offset ??= 0;

        var salesQuery = salesTrackDbContext.Sales
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.Product)
            .AsQueryable();

        if (query.QueryFilter != null)
        {
            if (query.QueryFilter.DateFrom.HasValue)
                salesQuery = salesQuery.Where(s => s.Date >= query.QueryFilter.DateFrom.Value.Date);

            if (query.QueryFilter.DateTo.HasValue)
                salesQuery = salesQuery.Where(s => s.Date <= query.QueryFilter.DateTo.Value.Date);

            if (query.QueryFilter.ProductId.HasValue)
                salesQuery = salesQuery.Where(s => s.SaleItems.Any(x => x.ProductId == query.QueryFilter.ProductId));

            if (!string.IsNullOrWhiteSpace(query.QueryFilter.Category))
                salesQuery = salesQuery.Where(s => s.SaleItems.Any(x => x.Product.Category.Name == query.QueryFilter.Category));
        }

        salesQuery = OrderByDirection(salesQuery, query.Direction);

        var sales = await salesQuery
            .Skip(query.Offset.Value)
            .Take(query.Limit.Value)
            .ToListAsync(cancellationToken);

        var result = sales.Select(sale => new GetSalesQueryResult
        {
            Date = sale.Date,
            TotalAmount = sale.TotalAmount,
            SaleItems = sale.SaleItems.Select(item => new GetSalesQueryResult.SaleItemInfoModel
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                TotalePrice = item.Quantity * item.Price
            }).ToList()
        }).ToList();

        return result;
    }

    private IQueryable<Sale> OrderByDirection(
        IQueryable<Sale> salesQuery,
        OrderDirectionEnum? direction)
    {
        return direction switch
        {
            OrderDirectionEnum.DESC => salesQuery.OrderByDescending(c => c.Date),
            _ => salesQuery.OrderBy(c => c.Date)
        };
    }
}

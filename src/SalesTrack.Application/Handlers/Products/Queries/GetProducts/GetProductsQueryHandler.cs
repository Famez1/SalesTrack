using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Application.Handlers.Categories.Queries;
using SalesTrack.Common.Models;
using SalesTrack.Domain.Entities;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Products.Queries.GetProducts;

public class GetProductsQueryHandler(
    ISalesTrackDbContext salesTrackDbContext, 
    IMapper mapper) : IRequestHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(
        GetProductsQuery query, CancellationToken cancellationToken)
    {
        query.Offset ??= 0;
        query.Limit ??= 20;

        var productQuery = salesTrackDbContext.Products
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(query.ProductName))
        {
            productQuery = productQuery.Where(x => EF.Functions.ILike(x.Name, $"%{query.ProductName}%"));
        }

        productQuery = OrderByDirection(productQuery, query.Direction);

        var products = await productQuery
            .Skip(query.Offset.Value)
            .Take(query.Limit.Value)
            .ToListAsync(cancellationToken);

        var result = mapper.Map<List<GetProductsQueryResult.ProductInfoModel>>(products);

        return new GetProductsQueryResult { Products = result };
    }

    private IQueryable<Product> OrderByDirection(
        IQueryable<Product> inventoryQuery,
        OrderDirectionEnum? direction)
    {
        return direction switch
        {
            OrderDirectionEnum.DESC => inventoryQuery.OrderByDescending(c => c.Name),
            _ => inventoryQuery.OrderBy(c => c.Name)
        };
    }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Common.Models;
using SalesTrack.Domain.Entities;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;

public class GetInventoriesQueryHandler(
    ISalesTrackDbContext salesTrackDbContext, 
    IMapper mapper) : IRequestHandler<GetInventoriesQuery, GetInventoriesQueryResult>
{
    public async Task<GetInventoriesQueryResult> Handle(
        GetInventoriesQuery query, 
        CancellationToken cancellationToken)
    {
        query.Offset ??= 0;
        query.Limit ??= 20;

        var inventoriesQuery = salesTrackDbContext.Inventories
            .Include(x => x.Product)
            .AsQueryable();

        if (query.Search is not null)
        {
            inventoriesQuery = inventoriesQuery
                .Where(x => EF.Functions.ILike(x.Product.Name, $"%{query.Search}%"));
        }

        inventoriesQuery = OrderByDirection(inventoriesQuery, query.Direction);

        var inventories = await inventoriesQuery
            .Skip(query.Offset.Value)
            .Take(query.Limit.Value)
            .Select(x => new Inventory
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Product = new Product
                {
                    Id = x.ProductId,
                    Name = x.Product.Name,
                }
            })
            .ToListAsync(cancellationToken);

        var result = mapper.Map<List<GetInventoriesQueryResult.InventoriesInfoModel>>(inventories);

        return new GetInventoriesQueryResult { Inventories = result };
    }

    private IQueryable<Inventory> OrderByDirection(
        IQueryable<Inventory> inventoryQuery,
        OrderDirectionEnum? direction)
    {
        return direction switch
        {
            OrderDirectionEnum.DESC => inventoryQuery.OrderByDescending(c => c.Product.Name),
            _ => inventoryQuery.OrderBy(c => c.Product.Name)
        };
    }
}

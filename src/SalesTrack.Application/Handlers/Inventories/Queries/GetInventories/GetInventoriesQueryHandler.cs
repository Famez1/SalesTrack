using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        var inventories = salesTrackDbContext.Inventories
            .Include(x => x.Product)
            .Skip(query.Offset.Value)
            .Take(query.Limit.Value)
            .Select(x => new Domain.Entities.Inventory
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Product = new Domain.Entities.Product
                {
                    Id = x.ProductId,
                    Name = x.Product.Name,
                }
            });

        var result = mapper.Map<List<GetInventoriesQueryResult.InventoriesInfoModel>>(inventories);

        return new GetInventoriesQueryResult { Inventories = result };
    }
}

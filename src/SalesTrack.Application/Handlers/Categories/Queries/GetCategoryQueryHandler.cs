using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Common.Models;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Categories.Queries;

public class GetCategoryQueryHandler(
    ISalesTrackDbContext salesTrackDbContext,
    IMapper mapper) : IRequestHandler<GetCategoryQuery, GetCategoryQueryResult>
{
    public async Task<GetCategoryQueryResult> Handle(
        GetCategoryQuery query, 
        CancellationToken cancellationToken)
    {
        query.Offset ??= 0;
        query.Limit ??= 20;

        var categoriesQuery = salesTrackDbContext.Categories.AsQueryable();

        categoriesQuery = OrderByDirection(categoriesQuery, query.Direction);

        var categories = await categoriesQuery
            .Skip(query.Offset.Value)
            .Take(query.Limit.Value)
            .ToListAsync(cancellationToken); 

        var result = mapper.Map<List<GetCategoryQueryResult.CategoryInfoModel>>(categories);

        return new GetCategoryQueryResult { Categories = result };
    }

    private IQueryable<Category> OrderByDirection(
        IQueryable<Category> inventoryQuery,
        OrderDirectionEnum? direction)
    {
        return direction switch
        {
            OrderDirectionEnum.DESC => inventoryQuery.OrderByDescending(c => c.Name),
            _ => inventoryQuery.OrderBy(c => c.Name)
        };
    }
}

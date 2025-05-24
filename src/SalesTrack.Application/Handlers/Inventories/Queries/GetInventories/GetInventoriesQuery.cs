using MediatR;
using SalesTrack.Common.Models;

namespace SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;

public class GetInventoriesQuery : BaseDtoModel, IRequest<GetInventoriesQueryResult>
{
    public string? Search {  get; set; }
}

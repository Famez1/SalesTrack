using MediatR;
using SalesTrack.Common.Models;

namespace SalesTrack.Application.Handlers.Categories.Queries;

public class GetCategoryQuery : BaseDtoModel, IRequest<GetCategoryQueryResult>
{
    public string Search { get; set; } = string.Empty;
}

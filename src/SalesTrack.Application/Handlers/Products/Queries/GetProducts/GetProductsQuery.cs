using MediatR;
using SalesTrack.Common.Models;

namespace SalesTrack.Application.Handlers.Products.Queries.GetProducts;

public class GetProductsQuery : BaseDtoModel, IRequest<GetProductsQueryResult>
{
    public string? ProductName { get; set; }
}

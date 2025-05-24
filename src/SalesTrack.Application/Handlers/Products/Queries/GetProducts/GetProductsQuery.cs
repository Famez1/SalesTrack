using MediatR;
using SalesTrack.Common.Models;

namespace SalesTrack.Application.Handlers.Products.Queries.GetProducts;

public class GetProductsQuery : BaseDtoModel, IRequest<GetProductsQueryResult>
{
    public Guid? CategoryId { get; set; }
}

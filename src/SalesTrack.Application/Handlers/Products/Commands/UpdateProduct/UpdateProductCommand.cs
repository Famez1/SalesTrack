using MediatR;

namespace SalesTrack.Application.Handlers.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }

    public decimal Price { get; set; }
}

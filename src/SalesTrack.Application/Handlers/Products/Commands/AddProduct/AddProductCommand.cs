using MediatR;

namespace SalesTrack.Application.Handlers.Products.Commands.AddProduct;

public class AddProductCommand : IRequest
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Unit { get; set; }

    public string CategoryName { get; set; }
}

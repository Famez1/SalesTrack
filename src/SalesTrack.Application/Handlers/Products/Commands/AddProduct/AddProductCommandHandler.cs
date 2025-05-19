using AutoMapper;
using MediatR;
using SalesTrack.Common.Exceptions;
using SalesTrack.Domain.Entities;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Products.Commands.AddProduct;

public class AddProductCommandHandler(
    ISalesTrackDbContext salesTrackDbContext,
    IMapper mapper) : IRequestHandler<AddProductCommand>
{
    public async Task Handle(
        AddProductCommand command, 
        CancellationToken cancellationToken)
    {
        var newProduct = mapper.Map<Product>(command);
        newProduct.Id = Guid.NewGuid();

        salesTrackDbContext.Products.Add(newProduct);

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }
}

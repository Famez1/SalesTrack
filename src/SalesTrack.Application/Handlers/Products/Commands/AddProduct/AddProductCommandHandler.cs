using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        newProduct.CategoryId = await salesTrackDbContext.Categories
            .Where(x => x.Name == command.CategoryName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        salesTrackDbContext.Products.Add(newProduct);

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }
}

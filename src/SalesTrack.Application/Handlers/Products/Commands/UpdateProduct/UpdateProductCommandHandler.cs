using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Common.Exceptions;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(ISalesTrackDbContext salesTrackDbContext) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(
        UpdateProductCommand command, 
        CancellationToken cancellationToken)
    {
        var product = await salesTrackDbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id);

        if (product is null)
        {
            throw new BadRequestException("Продукт не найден");
        }

        if (command.Price < 0)
        {
            throw new BadRequestException("Цена продукта не должна быть меньше нуля");
        }

        product.Price = command.Price;

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }
}

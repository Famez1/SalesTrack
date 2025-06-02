using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Domain.Entities;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Inventories.Commands.AddInventory;

public class AddInventoryCommandHandler(
    ISalesTrackDbContext salesTrackDbContext) : IRequestHandler<AddInventoryCommand>
{
    public async Task Handle(
        AddInventoryCommand command, 
        CancellationToken cancellationToken)
    {
        var inventory = await salesTrackDbContext.Inventories
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Product.Name == command.ProductName, cancellationToken);

        if (inventory is null)
        {
            await CreateInventory(command);
        }
        else
        {
            UpdateInventory(inventory, command);
        }

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateInventory(
        Inventory inventory, 
        AddInventoryCommand addInventoryCommand)
    {
        inventory.Quantity = inventory.Quantity + addInventoryCommand.Quantity;
    }

    private async Task CreateInventory(AddInventoryCommand addInventoryCommand)
    {
        var product = await salesTrackDbContext.Products
            .Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
            })
            .FirstOrDefaultAsync(x => x.Name == addInventoryCommand.ProductName);

        var inventory = new Inventory
        {
            Quantity = addInventoryCommand.Quantity,
            ProductId = product.Id,
        };

        salesTrackDbContext.Inventories.Add(inventory);
    }
}

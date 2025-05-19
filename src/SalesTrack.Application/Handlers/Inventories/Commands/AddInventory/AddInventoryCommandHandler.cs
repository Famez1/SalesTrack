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
            .FirstOrDefaultAsync(x => x.ProductId == command.ProductId, cancellationToken);

        if (inventory is null)
        {
            CreateInventory(command);
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

    private void CreateInventory(AddInventoryCommand addInventoryCommand)
    {
        var inventory = new Inventory
        {
            Quantity = addInventoryCommand.Quantity,
            ProductId = addInventoryCommand.ProductId,
        };

        salesTrackDbContext.Inventories.Add(inventory);
    }
}

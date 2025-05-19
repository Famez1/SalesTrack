using MediatR;

namespace SalesTrack.Application.Handlers.Inventories.Commands.AddInventory;

public class AddInventoryCommand : IRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}

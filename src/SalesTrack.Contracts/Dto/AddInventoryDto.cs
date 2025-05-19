namespace SalesTrack.Contracts.Dto;

public class AddInventoryDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}

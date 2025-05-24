namespace SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;

public class GetInventoriesQueryResult 
{
    public List<InventoriesInfoModel> Inventories { get; set; } = [];

    public class InventoriesInfoModel
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}

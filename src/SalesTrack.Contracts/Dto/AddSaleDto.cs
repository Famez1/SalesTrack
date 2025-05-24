namespace SalesTrack.Contracts.Dto;

public class AddSaleDto
{
    public List<SaleProductInfoModel> SaledProducts { get; set; } = [];

    public class SaleProductInfoModel
    {
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}

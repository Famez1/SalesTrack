namespace SalesTrack.Contracts.Dto;

public class GetSalesResponseDto
{
    public DateTime Date { get; set; }

    public decimal TotalAmount { get; set; }

    public List<SaleItemInfoModel> SaleItems { get; set; } = [];

    public class SaleItemInfoModel
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalePrice { get; set; }
    }
}

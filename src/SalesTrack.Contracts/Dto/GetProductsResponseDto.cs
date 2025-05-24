namespace SalesTrack.Contracts.Dto;

public class GetProductsResponseDto
{
    public List<ProductInfoModel> Products { get; set; } = [];

    public class ProductInfoModel
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public string Unit { get; set; }

        public string Name { get; set; }
    }
}

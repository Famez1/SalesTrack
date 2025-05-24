namespace SalesTrack.Application.Handlers.Products.Queries.GetProducts;

public class GetProductsQueryResult
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

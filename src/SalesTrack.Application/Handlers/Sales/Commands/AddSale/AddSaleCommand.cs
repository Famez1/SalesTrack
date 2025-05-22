using MediatR;

namespace SalesTrack.Application.Handlers.Sales.Commands.AddSale;

public class AddSaleCommand : IRequest
{
    public List<SaleProductInfoModel> SaledProducts { get; set; }

    public class SaleProductInfoModel
    {
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}

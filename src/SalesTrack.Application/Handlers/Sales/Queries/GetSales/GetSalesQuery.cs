using MediatR;
using SalesTrack.Common.Models;

namespace SalesTrack.Application.Handlers.Sales.Queries.GetSales;

public class GetSalesQuery : BaseDtoModel, IRequest<List<GetSalesQueryResult>>
{
    public Filter? QueryFilter { get; set; }

    public class Filter
    {
        public string Category { get; set; }

        public Guid? ProductId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}

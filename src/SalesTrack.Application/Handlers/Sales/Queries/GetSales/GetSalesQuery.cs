using MediatR;
using SalesTrack.Common.Models;

namespace SalesTrack.Application.Handlers.Sales.Queries.GetSales;

public class GetSalesQuery : IRequest<List<GetSalesQueryResult>>
{
    public int? Limit { get; set; }

    public int? Offset { get; set; }

    public OrderDirectionEnum? Direction { get; set; }

    public Filter? QueryFilter { get; set; }

    public class Filter
    {
        public string Category { get; set; }

        public Guid? ProductId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}

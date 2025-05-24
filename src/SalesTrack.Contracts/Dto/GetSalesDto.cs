using SalesTrack.Common.Models;

namespace SalesTrack.Contracts.Dto;

public class GetSalesDto
{
    public int? Limit { get; set; }

    public int? Offset { get; set; }

    public string? Direction { get; set; }

    public Filter? QueryFilter { get; set; }

    public class Filter
    {
        public string Category { get; set; }

        public Guid? ProductId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}

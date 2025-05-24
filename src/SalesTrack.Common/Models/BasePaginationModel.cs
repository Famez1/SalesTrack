namespace SalesTrack.Common.Models;

public abstract class BasePaginationModel
{
    public int? Limit { get; set; }

    public int? Offset { get; set; }
}

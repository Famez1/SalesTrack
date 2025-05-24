namespace SalesTrack.Common.Models;

public abstract class BaseDtoModel
{
    public int? Limit { get; set; }

    public OrderDirectionEnum? Direction { get; set; }  

    public int? Offset { get; set; }
}

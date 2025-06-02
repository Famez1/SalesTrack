using SalesTrack.Common.Models;

namespace SalesTrack.Contracts.Dto;

public class GetProductsDto : BaseDtoModel
{
    public string? CategoryName {  get; set; }
}

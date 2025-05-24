using SalesTrack.Common.Models;

namespace SalesTrack.Contracts.Dto;

public class GetProductsDto : BaseDtoModel
{
    public Guid? CategoryId {  get; set; }
}

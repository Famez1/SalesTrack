using SalesTrack.Common.Models;

namespace SalesTrack.Contracts.Dto;

public class GetInventoriesDto : BaseDtoModel
{
    public string Search {  get; set; }
}

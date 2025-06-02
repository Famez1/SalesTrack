using SalesTrack.Common.Models;

namespace SalesTrack.Contracts.Dto;

public class GetCategoriesDto : BaseDtoModel
{
    public string Search { get; set; } = string.Empty;
}

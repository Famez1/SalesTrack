namespace SalesTrack.Contracts.Dto;

public class GetCategoriesResponseDto 
{
    public List<CategoryInfoModel> Categories { get; set; } = [];

    public class CategoryInfoModel
    {
        public string Name { get; set; }
    }
}

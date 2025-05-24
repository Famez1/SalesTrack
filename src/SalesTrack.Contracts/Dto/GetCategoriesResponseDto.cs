namespace SalesTrack.Contracts.Dto;

public class GetCategoriesResponseDto 
{
    public List<CategoryInfoModel> Categories { get; set; } = [];

    public class CategoryInfoModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

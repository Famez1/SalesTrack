namespace SalesTrack.Application.Handlers.Categories.Queries;

public class GetCategoryQueryResult
{
    public List<CategoryInfoModel> Categories { get; set; } = [];

    public class CategoryInfoModel
    {
        public string Name { get; set; }
    }
}

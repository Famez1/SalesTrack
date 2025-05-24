namespace SalesTrack.Application.Handlers.Categories.Queries;

public class GetCategoryQueryResult
{
    public List<CategoryInfoModel> Categories { get; set; } = [];

    public class CategoryInfoModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

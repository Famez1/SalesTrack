using AutoMapper;
using SalesTrack.Application.Handlers.Categories.Queries;

namespace SalesTrack.Application.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, GetCategoryQueryResult.CategoryInfoModel>();
    }
}

using AutoMapper;
using SalesTrack.Application.Handlers.Categories.Queries;
using SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;
using SalesTrack.Domain.Entities;

namespace SalesTrack.Application.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, GetCategoryQueryResult.CategoryInfoModel>();
    }
}

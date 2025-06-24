using AutoMapper;
using SalesTrack.Application.Handlers.Categories.Commands;
using SalesTrack.Application.Handlers.Categories.Queries;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<GetCategoriesDto, GetCategoryQuery>();

        CreateMap<GetCategoryQueryResult, GetCategoriesResponseDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        CreateMap<GetCategoryQueryResult.CategoryInfoModel, GetCategoriesResponseDto.CategoryInfoModel>();

        CreateMap<AddCategoryDto, AddCategoryCommand>();
    }
}

using AutoMapper;
using SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile() 
    {
        CreateMap<GetInventoriesDto, GetInventoriesQuery>();

        CreateMap<GetInventoriesQueryResult, GetInventoriesResponseDto>()
            .ForMember(dest => dest.Inventories, opt => opt.MapFrom(src => src.Inventories));

        CreateMap<GetInventoriesQueryResult.InventoriesInfoModel, GetInventoriesResponseDto.InventoriesInfoModel>();
    }
}

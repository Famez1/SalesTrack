using AutoMapper;
using SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;
using SalesTrack.Domain.Entities;

namespace SalesTrack.Application.Mappings;

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        CreateMap<Inventory, GetInventoriesQueryResult.InventoriesInfoModel>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
    }
}

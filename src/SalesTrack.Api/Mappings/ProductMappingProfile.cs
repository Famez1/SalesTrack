using AutoMapper;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile() 
    {
        CreateMap<AddProductDto, AddProductCommand>();
    }
}

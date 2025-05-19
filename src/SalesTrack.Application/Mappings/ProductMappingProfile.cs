using AutoMapper;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Domain.Entities;

namespace SalesTrack.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<AddProductCommand, Product>();
    }
}

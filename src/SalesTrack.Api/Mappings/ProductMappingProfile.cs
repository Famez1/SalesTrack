using AutoMapper;
using SalesTrack.Application.Handlers.Categories.Queries;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Application.Handlers.Products.Queries.GetProducts;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile() 
    {
        CreateMap<AddProductDto, AddProductCommand>();

        CreateMap<GetProductsDto, GetProductsQuery>();

        CreateMap<GetProductsQueryResult, GetProductsResponseDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<GetProductsQueryResult.ProductInfoModel, GetProductsResponseDto.ProductInfoModel>();
    }
}

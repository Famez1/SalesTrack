using AutoMapper;
using SalesTrack.Application.Handlers.Sales.Commands.AddSale;
using SalesTrack.Application.Handlers.Sales.Queries.GetSales;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        CreateMap<AddSaleDto, AddSaleCommand>();

        CreateMap<AddSaleDto.SaleProductInfoModel, AddSaleCommand.SaleProductInfoModel>();

        CreateMap<GetSalesDto, GetSalesQuery>();

        CreateMap<GetSalesDto.Filter, GetSalesQuery.Filter>();

        CreateMap<GetSalesQueryResult, GetSalesResponseDto>();

        CreateMap<GetSalesQueryResult.SaleItemInfoModel, GetSalesResponseDto.SaleItemInfoModel>();
    }
}

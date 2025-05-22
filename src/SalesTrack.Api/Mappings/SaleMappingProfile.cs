using AutoMapper;
using SalesTrack.Application.Handlers.Sales.Commands.AddSale;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        CreateMap<AddSaleDto, AddSaleCommand>();

        CreateMap<AddSaleDto.SaleProductInfoModel, AddSaleCommand.SaleProductInfoModel>();
    }
}

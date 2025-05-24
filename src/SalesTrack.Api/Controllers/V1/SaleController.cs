using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Sales.Commands.AddSale;
using SalesTrack.Application.Handlers.Sales.Queries.GetSales;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class SaleController(
    IMediator mediator,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponseV1> AddSaleAsync(AddSaleDto addSaleDto)
    {
        await mediator.Send(mapper.Map<AddSaleCommand>(addSaleDto));

        return new ApiResponseV1();
    }

    [HttpGet]
    public async Task<ApiResponseV1<List<GetSalesResponseDto>>> GetSalesAsync([FromQuery] GetSalesDto getSalesDto)
    {
        var result = await mediator.Send(mapper.Map<GetSalesQuery>(getSalesDto));

        return new ApiResponseV1<List<GetSalesResponseDto>>
        {
            Data = mapper.Map<List<GetSalesResponseDto>>(result)
        };
    }
}

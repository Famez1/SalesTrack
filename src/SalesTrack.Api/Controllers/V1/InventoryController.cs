using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Inventories.Commands.AddInventory;
using SalesTrack.Application.Handlers.Inventories.Queries.GetInventories;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class InventoryController(
    IMediator mediator,
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Создать запись о пополнении товара
    /// </summary>
    /// <param name="addInventoryDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ApiResponseV1> AddInventoryAsync(
        [FromBody] AddInventoryDto addInventoryDto,
        CancellationToken cancellationToken)
    {
        var command = new AddInventoryCommand 
        { 
            ProductId = addInventoryDto.ProductId, 
            Quantity = addInventoryDto.Quantity 
        };

        await mediator.Send(command, cancellationToken);

        return new ApiResponseV1();
    }

    [HttpGet]
    public async Task<ApiResponseV1<GetInventoriesResponseDto>> GetInventoriesAsync(
        [FromQuery] GetInventoriesDto getInventoriesDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<GetInventoriesQuery>(getInventoriesDto), cancellationToken);

        return new ApiResponseV1<GetInventoriesResponseDto>
        {
            Data = mapper.Map<GetInventoriesResponseDto>(result)
        };
    }
}

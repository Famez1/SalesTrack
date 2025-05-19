using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Inventories.Commands.AddInventory;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class InventoryController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создать запись о пополнении товара
    /// </summary>
    /// <param name="addInventoryDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ApiResponseV1> AddInventoryAsync([FromBody] AddInventoryDto addInventoryDto)
    {
        var command = new AddInventoryCommand 
        { 
            ProductId = addInventoryDto.ProductId, 
            Quantity = addInventoryDto.Quantity 
        };

        await mediator.Send(command);

        return new ApiResponseV1();
    }
}

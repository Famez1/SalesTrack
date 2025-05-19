using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController(
    IMapper mapper,
    IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Добавить товар в список товаров
    /// </summary>
    [HttpPost]
    public async Task<ApiResponseV1> AddProductAsync([FromBody] AddProductDto addProductDto)
    {
        var command = mapper.Map<AddProductCommand>(addProductDto);

        await mediator.Send(command);

        return new ApiResponseV1();
    }
}

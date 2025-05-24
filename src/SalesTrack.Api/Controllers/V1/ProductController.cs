using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Application.Handlers.Products.Commands.UpdateProduct;
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
    [Authorize] 
    [HttpPost]
    public async Task<ApiResponseV1> AddProductAsync([FromBody] AddProductDto addProductDto)
    {
        var command = mapper.Map<AddProductCommand>(addProductDto);

        await mediator.Send(command);

        return new ApiResponseV1();
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ApiResponseV1> UpdateProductAsync(
        [FromRoute] Guid id, 
        [FromBody] UpdateProductDto updateProductDto, 
        CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProductCommand { Id = id, Price = updateProductDto.Price }, cancellationToken);

        return new ApiResponseV1();
    }
}

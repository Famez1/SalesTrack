using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Categories.Commands;
using SalesTrack.Application.Handlers.Categories.Queries;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoryController(
    IMediator mediator, 
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Добавить категорию
    /// </summary>
    /// <param name="addCategoryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ApiResponseV1> AddCategoryAsync(
        [FromBody] AddCategoryDto addCategoryDto, 
        CancellationToken cancellationToken)
    {
        await mediator.Send(new AddCategoryCommand { Name = addCategoryDto.Name }, cancellationToken);

        return new ApiResponseV1();
    }

    /// <summary>
    /// Получить список категорий
    /// </summary>
    /// <param name="getCategoriesDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ApiResponseV1<GetCategoriesResponseDto>> GetCategoriesAsync(
        [FromQuery] GetCategoriesDto getCategoriesDto)
    {
        var result = await mediator.Send(mapper.Map<GetCategoryQuery>(getCategoriesDto));

        return new ApiResponseV1<GetCategoriesResponseDto>
        {
            Data = mapper.Map<GetCategoriesResponseDto>(result)
        };
    }
}

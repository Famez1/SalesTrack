using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.Api.Contracts;
using SalesTrack.Application.Handlers.Categories.Commands;
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
    [Authorize]
    [HttpPost]
    public async Task<ApiResponseV1> AddCategoryAsync([FromBody] AddCategoryDto addCategoryDto)
    {
        await mediator.Send(new AddCategoryCommand { Name = addCategoryDto.Name });

        return new ApiResponseV1();
    }
}

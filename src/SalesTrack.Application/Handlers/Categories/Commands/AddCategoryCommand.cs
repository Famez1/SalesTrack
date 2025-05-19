using MediatR;

namespace SalesTrack.Application.Handlers.Categories.Commands;

public class AddCategoryCommand : IRequest
{
    public string Name { get; set; }
}

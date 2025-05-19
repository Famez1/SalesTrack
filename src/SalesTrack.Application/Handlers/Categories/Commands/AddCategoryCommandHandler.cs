using MediatR;
using SalesTrack.Common.Exceptions;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Categories.Commands;

public class AddCategoryCommandHandler(ISalesTrackDbContext salesTrackDbContext) : IRequestHandler<AddCategoryCommand>
{
    public async Task Handle(
        AddCategoryCommand command, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.Name))
        {
            throw new BadRequestException("Название категории не должно быть пустым");
        }

        var category = new Category
        {
            Name = command.Name,
        };

        salesTrackDbContext.Categories.Add(category);

        await salesTrackDbContext.SaveChangesAsync(cancellationToken);
    }
}

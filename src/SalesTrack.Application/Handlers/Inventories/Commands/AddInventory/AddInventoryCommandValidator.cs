using FluentValidation;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Inventories.Commands.AddInventory;

public class AddInventoryCommandValidator : AbstractValidator<AddInventoryCommand>
{
    private readonly ISalesTrackDbContext _salesTrackDbContext;

    public AddInventoryCommandValidator(ISalesTrackDbContext salesTrackDbContext)
    {
        _salesTrackDbContext = salesTrackDbContext;

        RuleFor(x => x.ProductId)
            .Must(IsPruductExists)
            .WithMessage("Такого товара не существует");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Кол-во единиц товара должно быть больше 0");
    }

    private bool IsPruductExists(Guid productId)
    {
        return _salesTrackDbContext.Products.Any(x => x.Id == productId);
    }
}

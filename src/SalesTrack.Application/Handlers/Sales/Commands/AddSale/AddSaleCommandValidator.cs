using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SalesTrack.Persistence;

namespace SalesTrack.Application.Handlers.Sales.Commands.AddSale;

public class AddSaleCommandValidator : AbstractValidator<AddSaleCommand>
{
    private readonly ISalesTrackDbContext _salesTrackDbContext;

    public AddSaleCommandValidator(ISalesTrackDbContext salesTrackDbContext)
    {
        _salesTrackDbContext = salesTrackDbContext;

        RuleForEach(x => x.SaledProducts).ChildRules(product =>
        {
            product.RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId обязателен.")
                .MustAsync(ProductExists).WithMessage("Товар с таким ProductId не существует.");

            product.RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Количество должно быть больше нуля.");

            product.RuleFor(x => x)
                .MustAsync(EnoughInInventory)
                .WithMessage("Недостаточно товара на складе.");
        });
    }

    private async Task<bool> ProductExists(Guid productId, CancellationToken cancellationToken)
    {
        return await _salesTrackDbContext.Products
            .AnyAsync(p => p.Id == productId, cancellationToken);
    }

    private async Task<bool> EnoughInInventory(AddSaleCommand.SaleProductInfoModel product, CancellationToken cancellationToken)
    {
        var inventory = await _salesTrackDbContext.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == product.ProductId, cancellationToken);

        if (inventory == null)
            return false; 

        return inventory.Quantity >= product.Quantity;
    }
}

using FluentValidation;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Persistence;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    private readonly ISalesTrackDbContext _salesTrackDbContext;

    public AddProductCommandValidator(ISalesTrackDbContext salesTrackDbContext)
    {
        _salesTrackDbContext = salesTrackDbContext;

        RuleFor(x => x.CategoryId)
            .Must(IsCategoryExists)
            .WithMessage("Категория не существует");

        RuleFor(x => x.Name)
            .Must(IsProductExists)
            .WithMessage("Такой продукт уже существует, его не нужно добавлять");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Цена должна быть больше 0");
    }

    private bool IsCategoryExists(Guid categoryId)
    {
        return _salesTrackDbContext.Categories.Any(x => x.Id == categoryId);
    }

    private bool IsProductExists(string productName)
    {
        return !_salesTrackDbContext.Products.Any(x => x.Name == productName);
    }
}

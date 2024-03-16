using FluentValidation;
using FullCart.Server.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateProduct;

public class AddUpdateProductCommandValidator : AbstractValidator<AddUpdateProductCommand>
{
    public AddUpdateProductCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(m => m)
            .MustAsync(async (m, cancellation)
                => !await appDbContext.Products.AnyAsync(p => p.Name.ToLower() == m.Name.ToLower() &&
                (!m.Id.HasValue || m.Id.Value != p.Id), cancellationToken: cancellation))
            .WithMessage("Duplicate product name")
            .WithName(m => nameof(m.Name));


        RuleFor(m => m.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(m => m.Price)
            .InclusiveBetween(1, 10000000.99M);

        RuleFor(m => m.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(m => m.CategoryId)
            .GreaterThan(0)
            .MustAsync(async (categoryId, cancellation) => await appDbContext.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken: cancellation))
            .WithMessage("Invalid category");

        RuleFor(m => m.BrandId)
            .GreaterThan(0)
            .MustAsync(async (brandId, cancellation) => await appDbContext.Brands.AnyAsync(c => c.Id == brandId, cancellationToken: cancellation))
            .WithMessage("Invalid brand");
    }
}

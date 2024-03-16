using FluentValidation;
using FullCart.Server.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateBrand;

public class AddUpdateBrandCommandValidator : AbstractValidator<AddUpdateBrandCommand>
{
    public AddUpdateBrandCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(m => m)
            .MustAsync(async (m, cancellation)
                => !await appDbContext.Brands.AnyAsync(c =>
                    c.Name.ToLower() == m.Name.ToLower() &&
                    (!m.Id.HasValue || m.Id.Value != c.Id), cancellationToken: cancellation))
            .WithMessage("Duplicate brand name")
            .WithName("Name");
    }
}

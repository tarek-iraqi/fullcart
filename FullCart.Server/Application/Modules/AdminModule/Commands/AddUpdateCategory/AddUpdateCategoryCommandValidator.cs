using FluentValidation;
using FullCart.Server.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateCategory;

public class AddUpdateCategoryCommandValidator : AbstractValidator<AddUpdateCategoryCommand>
{
    public AddUpdateCategoryCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(m => m)
            .MustAsync(async (m, cancellation)
                => !await appDbContext.Categories.AnyAsync(c =>
                    c.Name.ToLower() == m.Name.ToLower() &&
                    (!m.Id.HasValue || m.Id.Value != c.Id), cancellationToken: cancellation))
            .WithMessage("Duplicate category name")
            .WithName("Name");
    }
}

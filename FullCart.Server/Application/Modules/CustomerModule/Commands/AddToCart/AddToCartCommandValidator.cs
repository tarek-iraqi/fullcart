using FluentValidation;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(m => m.ProductId)
            .NotEmpty();

        RuleFor(m => m.Quantity)
            .GreaterThan(0);
    }
}

using FluentValidation;

namespace FullCart.Server.Application.Modules.CommonModule.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.Username)
            .NotEmpty();

        RuleFor(p => p.Password)
            .NotEmpty();
    }
}

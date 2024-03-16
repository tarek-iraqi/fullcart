using FluentValidation;
using FullCart.Server.Application.Contracts;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CommonModule.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result>
{
    private readonly AppDbContext _appDbContext;
    private readonly IValidator<LoginCommand> _validator;
    private readonly IApplicationConfiguration _applicationConfiguration;

    public LoginHandler(AppDbContext appDbContext,
        IValidator<LoginCommand> validator,
        IApplicationConfiguration applicationConfiguration)
    {
        _appDbContext = appDbContext;
        _validator = validator;
        _applicationConfiguration = applicationConfiguration;
    }
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = _validator.Validate(request);

        if (validatorResult.IsValid is false)
            return ApiResult.Fail(validatorResult.Errors);

        var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
            return ApiResult.Fail(new ErrorResult("user", "invalid username or password"));

        if (SecurityHelper.VerifyPassword(request.Password, user.Password) is false)
            return ApiResult.Fail(new ErrorResult("user", "invalid username or password"));

        var token = SecurityHelper.GenerateAccessToken(user.Id, user.Username,
            user.Role.ToString(), _applicationConfiguration.GetAppSettings().JWTSettings);

        return ApiResult.Success(new LoginResponseDTO
        {
            AccessToken = token,
            Username = user.Username,
            ExpiresIn = _applicationConfiguration.GetAppSettings().JWTSettings.DurationInMillisecond,
            Role = user.Role.ToString()
        });
    }
}

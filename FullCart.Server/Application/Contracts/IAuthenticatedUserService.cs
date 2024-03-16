namespace FullCart.Server.Application.Contracts;

public interface IAuthenticatedUserService
{
    string? UserId { get; }
    string? Username { get; }
}

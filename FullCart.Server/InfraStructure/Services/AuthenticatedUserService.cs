using FullCart.Server.Application.Contracts;
using FullCart.Server.Shared.Constants;
using System.Security.Claims;

namespace FullCart.Server.InfraStructure.Services;

public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    public string? UserId { get; } = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? Username { get; } = httpContextAccessor.HttpContext?.User?.FindFirst(KeyValueConstants.UsernameClaimType)?.Value;
}

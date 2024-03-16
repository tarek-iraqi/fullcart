using FullCart.Server.Application.Modules.CommonModule.Commands.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Server.Controllers;

public class UsersController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
        => EndpointResult(await _mediator.Send(loginCommand));
}

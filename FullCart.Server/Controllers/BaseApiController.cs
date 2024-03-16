using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected IMediator _mediator => HttpContext.RequestServices.GetService<IMediator>()!;

    protected IActionResult EndpointResult(Result result)
        => result.IsSuccess ? Ok(result) : BadRequest(result);
}

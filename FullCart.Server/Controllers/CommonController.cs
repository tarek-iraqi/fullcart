using FullCart.Server.Application.Modules.CommonModule.Queries.GetBrandLookup;
using FullCart.Server.Application.Modules.CommonModule.Queries.GetCategoryLookup;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Server.Controllers;

public class CommonController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> CategoryLookup()
        => EndpointResult(await _mediator.Send(new GetCategoryLookupQuery()));


    [HttpGet]
    public async Task<IActionResult> BrandLookup()
        => EndpointResult(await _mediator.Send(new GetBrandLookupQuery()));
}

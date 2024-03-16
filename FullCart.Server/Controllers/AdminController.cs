using FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateBrand;
using FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateCategory;
using FullCart.Server.Application.Modules.AdminModule.Commands.AddUpdateProduct;
using FullCart.Server.Application.Modules.AdminModule.Queries.GetBrands;
using FullCart.Server.Application.Modules.AdminModule.Queries.GetCategories;
using FullCart.Server.Application.Modules.AdminModule.Queries.GetOrders;
using FullCart.Server.Application.Modules.AdminModule.Queries.GetProduct;
using FullCart.Server.Application.Modules.AdminModule.Queries.GetProducts;
using FullCart.Server.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Server.Controllers;

[Authorize(policy: AuthorizationPolicies.AdminPolicy)]
public class AdminController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> UpsertCategory(AddUpdateCategoryCommand command)
        => EndpointResult(await _mediator.Send(command));

    [HttpPost]
    public async Task<IActionResult> UpsertBrand(AddUpdateBrandCommand command)
        => EndpointResult(await _mediator.Send(command));

    [HttpPost]
    public async Task<IActionResult> UpsertProduct(AddUpdateProductCommand command)
        => EndpointResult(await _mediator.Send(command));

    [HttpGet("{id}")]
    public async Task<IActionResult> Product(Guid id)
        => EndpointResult(await _mediator.Send(new GetProductQuery(id)));

    [HttpGet]
    public async Task<IActionResult> ProductList([FromQuery]int pageNumber, [FromQuery]int pageSize)
        => EndpointResult(await _mediator.Send(new GetProductsQuery(pageSize, pageNumber)));

    [HttpGet]
    public async Task<IActionResult> OrderList([FromQuery] int pageNumber, [FromQuery] int pageSize)
        => EndpointResult(await _mediator.Send(new GetOrdersQuery(pageSize, pageNumber)));

    [HttpGet]
    public async Task<IActionResult> CategoryList([FromQuery] int pageNumber, [FromQuery] int pageSize)
        => EndpointResult(await _mediator.Send(new GetCategoriesQuery(pageNumber, pageSize)));

    [HttpGet]
    public async Task<IActionResult> BrandList([FromQuery] int pageNumber, [FromQuery] int pageSize)
        => EndpointResult(await _mediator.Send(new GetBrandsQuery(pageNumber, pageSize)));
}

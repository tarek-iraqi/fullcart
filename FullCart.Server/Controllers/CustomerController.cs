using FullCart.Server.Application.Modules.CustomerModule.Commands.AddToCart;
using FullCart.Server.Application.Modules.CustomerModule.Commands.CancelOrder;
using FullCart.Server.Application.Modules.CustomerModule.Commands.PlaceOrder;
using FullCart.Server.Application.Modules.CustomerModule.Commands.RemoveFromCart;
using FullCart.Server.Application.Modules.CustomerModule.Queries.GetCart;
using FullCart.Server.Application.Modules.CustomerModule.Queries.GetOrders;
using FullCart.Server.Application.Modules.CustomerModule.Queries.GetProducts;
using FullCart.Server.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Server.Controllers;

[Authorize(policy: AuthorizationPolicies.CustomerPolicy)]
public class CustomerController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> ProductList([FromQuery] GetProductsQuery query)
    => EndpointResult(await _mediator.Send(query));

    [HttpGet]
    public async Task<IActionResult> OrderList([FromQuery] int pageNumber, [FromQuery] int pageSize)
        => EndpointResult(await _mediator.Send(new GetOrdersQuery(pageNumber, pageSize)));

    [HttpGet]
    public async Task<IActionResult> Cart()
        => EndpointResult(await _mediator.Send(new GetCartQuery()));

    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartCommand command)
        => EndpointResult(await _mediator.Send(command));

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(RemoveFromCartCommand command)
        => EndpointResult(await _mediator.Send(command));

    [HttpPost]
    public async Task<IActionResult> PlaceOrder()
        => EndpointResult(await _mediator.Send(new PlaceOrderCommand()));

    [HttpPost]
    public async Task<IActionResult> CancelOrder(CancelOrderCommand command)
        => EndpointResult(await _mediator.Send(command));
}

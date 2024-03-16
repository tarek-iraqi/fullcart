using FullCart.Server.Application.Contracts;
using FullCart.Server.Domain.Entities;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.PlaceOrder;

public class PlaceOrderHandler(AppDbContext appDbContext,
    IAuthenticatedUserService authenticatedUserService) : IRequestHandler<PlaceOrderCommand, Result>
{
    public  async Task<Result> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var cartItems = await appDbContext.CartItem
            .Include(ci => ci.Product)
            .Where(ci => ci.CustomerId.ToString() == authenticatedUserService.UserId)
            .ToListAsync(cancellationToken: cancellationToken);

        var invalidItems = new List<string>();
        var orderItems = new List<OrderItem>();
        

        foreach (var cartItem in cartItems)
        {
            if (cartItem.Quantity > cartItem.Product.Quantity)
                invalidItems.Add($"{cartItem.Product.Name} does not have engouh quantity, remove to continue.");
            else
            {
                orderItems.Add(OrderItem.Create(cartItem.Product, cartItem.Quantity));

                cartItem.Product.DecreaseQuantity(cartItem.Quantity);
            }
        }

        if (invalidItems.Count > 0)
            return ApiResult.Fail(invalidItems.Select(ii => new ErrorResult("Quantity", ii)).ToArray());

        var order = Order.Create(Guid.Parse(authenticatedUserService.UserId!), [.. orderItems]);

        appDbContext.Orders.Add(order);

        appDbContext.CartItem.RemoveRange(cartItems);

        await appDbContext.SaveChangesAsync(cancellationToken);

        return ApiResult.Success();
    }
}
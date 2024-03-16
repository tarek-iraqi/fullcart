using FullCart.Server.Application.Contracts;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetCart;

public class GetCartHandler(AppDbContext appDbContext,
    IAuthenticatedUserService authenticatedUserService) : IRequestHandler<GetCartQuery, Result>
{
    public async Task<Result> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await appDbContext.CartItem
            .Where(ci => ci.CustomerId.ToString() == authenticatedUserService.UserId)
            .Include(ci => ci.Product)
            .Select(ci => new CartResponseDto
            {
                ProductId = ci.ProductId,
                Price = ci.Product.Price,
                Quantity = ci.Quantity,
                ProductName = ci.Product.Name,
                TotalPrice = ci.Price
            }).ToListAsync();

        return ApiResult.Success(cartItems);
    }
}

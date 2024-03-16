using FullCart.Server.Application.Contracts;
using FullCart.Server.Domain.Entities;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.AddToCart;

public class AddToCartHandler(AppDbContext appDbContext,
    IAuthenticatedUserService authenticatedUserService) : IRequestHandler<AddToCartCommand, Result>
{
    public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);

        if (product is null)
            return ApiResult.Fail(new ErrorResult(nameof(request.ProductId), "Product not found"));

        if (product.Quantity < request.Quantity)
            return ApiResult.Fail(new ErrorResult(nameof(request.Quantity), "Invalid quantity"));

        var cartItem = await appDbContext.CartItem
            .FirstOrDefaultAsync(ci => ci.ProductId == request.ProductId &&
                                       ci.CustomerId.ToString() == authenticatedUserService.UserId, cancellationToken: cancellationToken);

        if(cartItem != null)
        {
            cartItem.UpdateQuantity(request.Quantity);

            if(product.Quantity < cartItem.Quantity)
                return ApiResult.Fail(new ErrorResult(nameof(request.Quantity), "Invalid quantity"));
        }
        else
        {
            cartItem = CartItem.Create(product.Id, request.Quantity, Guid.Parse(authenticatedUserService.UserId!));

            appDbContext.CartItem.Add(cartItem);
        }

        await appDbContext.SaveChangesAsync(cancellationToken);

        var numberOfProducts = await appDbContext.CartItem
            .CountAsync(ci =>  ci.CustomerId.ToString() == authenticatedUserService.UserId, cancellationToken: cancellationToken);

        return ApiResult.Success(numberOfProducts);
    }
}

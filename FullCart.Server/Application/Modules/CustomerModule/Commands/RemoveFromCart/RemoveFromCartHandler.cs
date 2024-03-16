using FullCart.Server.Application.Contracts;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.RemoveFromCart;

public class RemoveFromCartHandler(AppDbContext appDbContext,
    IAuthenticatedUserService authenticatedUserService) : IRequestHandler<RemoveFromCartCommand, Result>
{
    public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await appDbContext.CartItem
            .FirstOrDefaultAsync(ci => ci.ProductId == request.ProductId &&
                                       ci.CustomerId.ToString() == authenticatedUserService.UserId);

        if(cartItem != null)
            appDbContext.Remove(cartItem);

        await appDbContext.SaveChangesAsync();

        return ApiResult.Success();
    }
}

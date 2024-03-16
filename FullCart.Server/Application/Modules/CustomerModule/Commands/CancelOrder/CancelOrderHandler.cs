using FullCart.Server.Application.Contracts;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.CancelOrder;

public class CancelOrderHandler(AppDbContext appDbContext,
    IAuthenticatedUserService authenticatedUserService) : IRequestHandler<CancelOrderCommand, Result>
{
    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await appDbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && 
                                      o.CustomerId.ToString() == authenticatedUserService.UserId);

        if (order == null) 
            return ApiResult.Fail(new ErrorResult(nameof(request.OrderId), "Order not found"));

        order.CancelOrder();

        await appDbContext.SaveChangesAsync();

        return ApiResult.Success();
    }
}

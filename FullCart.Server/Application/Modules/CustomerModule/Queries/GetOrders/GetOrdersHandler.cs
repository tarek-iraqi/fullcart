using FullCart.Server.Application.Contracts;
using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Extensions;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetOrders;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, Result>
{
    private readonly AppDbContext _appDbContext;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public GetOrdersHandler(AppDbContext appDbContext,
        IAuthenticatedUserService authenticatedUserService)
    {
        _appDbContext = appDbContext;
        _authenticatedUserService = authenticatedUserService;
    }
    public async Task<Result> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Orders
            .Where(o => o.CustomerId.ToString() == _authenticatedUserService.UserId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderListDto
            {
                Id = o.Id,
                Status = o.Status.ToString(),
                Total = o.TotalPrice,
                CreatedAt = o.CreatedAt,
                OrderItems = o.Items.Select(i => new OrderItemDto
                {
                    Price = i.Price,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice,
                    ProductName = i.ProductName,
                }).ToList()
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResult.Success(result);
    }
}

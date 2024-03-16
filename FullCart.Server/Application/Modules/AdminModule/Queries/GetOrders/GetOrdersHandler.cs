using FullCart.Server.Persistence.Context;
using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Extensions;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetOrders;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, Result>
{
    private readonly AppDbContext _appDbContext;

    public GetOrdersHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Result> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Orders
            .Select(o => new OrderListDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt ?? default,
                Status = o.Status.ToString(),
                CustomerEmail = o.Customer.Email,
                Total = o.TotalPrice,
                OrderItems = o.Items.Select(i => new OrderItemDto
                {
                    Price = i.Price,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice,
                    ProductName = i.ProductName,
                }).ToList()
            }).OrderByDescending(i => i.CreatedAt)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResult.Success(result);
    }
}

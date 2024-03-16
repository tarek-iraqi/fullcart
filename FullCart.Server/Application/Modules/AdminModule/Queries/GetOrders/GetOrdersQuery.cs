using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.AdminModule.Queries.GetOrders;

public record GetOrdersQuery(int PageNumber, int PageSize)
    : IRequest<Result>;

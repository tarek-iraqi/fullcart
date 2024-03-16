using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Queries.GetCart;

public record GetCartQuery : IRequest<Result>;

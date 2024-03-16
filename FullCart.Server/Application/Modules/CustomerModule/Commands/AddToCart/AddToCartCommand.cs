using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.AddToCart;

public record AddToCartCommand(Guid ProductId,
    int Quantity) : IRequest<Result>;

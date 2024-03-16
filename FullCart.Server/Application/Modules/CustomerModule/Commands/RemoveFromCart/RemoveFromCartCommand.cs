using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.RemoveFromCart;

public record RemoveFromCartCommand(Guid ProductId) : IRequest<Result>;

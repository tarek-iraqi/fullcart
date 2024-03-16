using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : IRequest<Result>;

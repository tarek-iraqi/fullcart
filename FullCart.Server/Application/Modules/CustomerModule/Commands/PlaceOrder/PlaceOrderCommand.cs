using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CustomerModule.Commands.PlaceOrder;

public record PlaceOrderCommand() : IRequest<Result>;

using FullCart.Server.Shared.BaseModels;
using MediatR;

namespace FullCart.Server.Application.Modules.CommonModule.Commands.Login;

public record LoginCommand(string Username, string Password) : IRequest<Result>;

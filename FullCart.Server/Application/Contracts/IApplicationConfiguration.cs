using FullCart.Server.Shared.BaseModels;

namespace FullCart.Server.Application.Contracts;

public interface IApplicationConfiguration
{
    AppSettings GetAppSettings();
}

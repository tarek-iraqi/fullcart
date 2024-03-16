using FullCart.Server.Application.Contracts;
using FullCart.Server.Shared.BaseModels;
using Microsoft.Extensions.Options;

namespace FullCart.Server.InfraStructure.Services;

public class ApplicationConfiguration : IApplicationConfiguration
{
    private readonly AppSettings _appSettings;

    public ApplicationConfiguration(IOptions<AppSettings> options)
    {
        _appSettings = options.Value;
    }
    public AppSettings GetAppSettings() => _appSettings;
}

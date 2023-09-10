using Transportathon._0x80072F78.Core.Options;
using Transportathon._0x80072F78.Shared.Constants;
using Transportathon._0x80072F78.Shared.Options;

namespace Transportathon._0x80072F78.API.Extensions;

public static class StartupExtensions
{
    public static void AddExConfigOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomTokenOptions>(configuration.GetSection(ConfigurationSectionConst.TokenOptions));
        services.Configure<List<Client>>(configuration.GetSection(ConfigurationSectionConst.Clients));
    }
}
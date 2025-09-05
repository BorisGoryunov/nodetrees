using Microsoft.Extensions.DependencyInjection;
using Valetax.App.Services;

namespace Valetax.App;

public static class Installer
{
    public static IServiceCollection AddValetaxApp(this IServiceCollection services)
    {
        services.AddScoped<NodeService>();
        services.AddScoped<TreeService>();
        services.AddScoped<TokenService>();
        return services;
    }
}

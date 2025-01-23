using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Persistence.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddMemoryCache();
        // Add repositories here
        return services;
    }
}
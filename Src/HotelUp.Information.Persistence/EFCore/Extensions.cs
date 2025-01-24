using HotelUp.Information.Persistence.EFCore.Health;
using HotelUp.Information.Persistence.EFCore.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Persistence.EFCore;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.ConfigurePostgres();
        services.AddPostgres<AppDbContext>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database");
        return services;
    }
}
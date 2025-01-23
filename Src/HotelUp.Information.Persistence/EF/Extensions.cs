using HotelUp.Information.Persistence.EF.Health;
using HotelUp.Information.Persistence.EF.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Persistence.EF;

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
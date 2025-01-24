using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Persistence.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IHotelEventRepository, HotelEventRepository>();
        services.AddScoped<IPlannedDishRepository, PlannedDishRepository>();
        services.AddScoped<IRoomInformationRepository, RoomInformationRepository>();
        return services;
    }
}
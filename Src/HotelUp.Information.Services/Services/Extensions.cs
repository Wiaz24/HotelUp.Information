using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Services.Services;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHotelEventService, HotelEventService>();
        services.AddScoped<IPlannedDishService, PlannedDishService>();
        services.AddScoped<IRoomInformationService, RoomInformationService>();
        return services;
    }
}
using HotelUp.Information.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Services;

public static class Extensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddApplicationServices();
        return services;
    }
}
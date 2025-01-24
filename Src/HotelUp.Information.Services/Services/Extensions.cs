﻿using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Information.Services.Services;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHotelEventService, HotelEventService>();
        return services;
    }
}
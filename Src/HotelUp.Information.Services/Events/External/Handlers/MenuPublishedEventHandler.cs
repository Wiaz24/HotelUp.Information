using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Services.Services;
using HotelUp.Kitchen.Services.Events;
using MassTransit;

namespace HotelUp.Information.Services.Events.External.Handlers;

public class MenuPublishedEventHandler : IConsumer<MenuPublishedEvent>
{
    IPlannedDishService _plannedDishService;

    public MenuPublishedEventHandler(IPlannedDishService plannedDishService)
    {
        _plannedDishService = plannedDishService;
    }

    public async Task Consume(ConsumeContext<MenuPublishedEvent> context)
    {
        var plannedDishes = context.Message.MenuItems.Select(x => new PlannedDish
        {
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            ServingDate = context.Message.ServingDate
        });
        await _plannedDishService.AddDishesAsync(plannedDishes);
    }
}
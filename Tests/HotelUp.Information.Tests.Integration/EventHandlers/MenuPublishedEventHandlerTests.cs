using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;
using HotelUp.Information.Services.Events.External.DTOs;
using HotelUp.Kitchen.Services.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit.Abstractions;

namespace HotelUp.Information.Tests.Integration.EventHandlers;

[Collection(nameof(MenuPublishedEventHandlerTests))]
public class MenuPublishedEventHandlerTests : IntegrationTestsBase
{
    private readonly IBus _bus;
    
    public MenuPublishedEventHandlerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper) 
        : base(factory, testOutputHelper, "")
    {
        _bus = ServiceProvider.GetRequiredService<IBus>();
    }
    
    private async Task<List<PlannedDish>> GetPlannedDishesAsync(DateOnly servingDate)
    {
        using var scope = ServiceProvider.CreateScope();
        var plannedDishService = scope.ServiceProvider.GetRequiredService<IPlannedDishRepository>();
        return (await plannedDishService.GetByServingDateAsync(servingDate)).ToList();
    }
    
    [Fact]
    public async Task HandleAsync_WhenMenuIsPublished_ShouldCreatePlannedDishes()
    {
        // Arrange
        var menuId = Guid.NewGuid();
        var servingDate = DateOnly.FromDateTime(DateTime.UtcNow);
        var menuPublishedEvent = new MenuPublishedEvent
        {
            ServingDate = servingDate,
            MenuItems = new List<MenuItem>
            {
                new MenuItem
                {
                    Name = "Dish 1",
                    ImageUrl = "https://example.com/dish1.jpg"
                },
                new MenuItem
                {
                    Name = "Dish 2",
                    ImageUrl = "https://example.com/dish2.jpg"
                }
            }
        };
        
        // Act
        await _bus.Publish(menuPublishedEvent);
        await Task.Delay(500);
        
        
        // Assert
        var plannedDishes = await GetPlannedDishesAsync(servingDate);
        plannedDishes.ShouldNotBeEmpty();
        plannedDishes.ShouldContain(x => x.Name == "Dish 1");
        plannedDishes.ShouldContain(x => x.Name == "Dish 2");
        
        plannedDishes = await GetPlannedDishesAsync(servingDate.AddDays(1));
        plannedDishes.ShouldBeEmpty();
    }
}
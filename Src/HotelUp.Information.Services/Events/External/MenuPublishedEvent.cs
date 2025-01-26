using HotelUp.Information.Services.Events.External.DTOs;

// ReSharper disable once CheckNamespace
namespace HotelUp.Kitchen.Services.Events;

public record MenuPublishedEvent
{
    public required DateOnly ServingDate { get; init; }
    public required IEnumerable<MenuItem> MenuItems { get; init; }
}
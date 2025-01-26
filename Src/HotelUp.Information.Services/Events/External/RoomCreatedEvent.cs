using HotelUp.Information.Services.Events.External.DTOs;

// ReSharper disable once CheckNamespace
namespace HotelUp.Customer.Application.Events;

public record RoomCreatedEvent
{
    public int Id { get; init; }
    public int Capacity { get; init; }
    public int Floor { get; init; }
    public bool WithSpecialNeeds { get; init; }
    public RoomType Type { get; init; }
    public required string ImageUrl { get; init; }
}
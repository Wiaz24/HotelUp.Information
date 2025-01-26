using HotelUp.Information.Services.Events.External.DTOs;

// ReSharper disable once CheckNamespace
namespace HotelUp.Customer.Application.Events;

public record ReservationCreatedEvent
{
    public Guid ReservationId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public required List<RoomDto> Rooms { get; init; }
}
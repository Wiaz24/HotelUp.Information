// ReSharper disable once CheckNamespace
namespace HotelUp.Customer.Application.Events;

public record ReservationCanceledEvent
{
    public Guid ReservationId { get; init; }
}
namespace HotelUp.Information.Persistence.Entities;

public class RoomInformation
{
    public required int Number { get; init; }
    public required int Capacity { get; init; }
    public required bool WithSpecialNeeds { get; init; }
    public required string ImageUrl { get; init; }
    public required List<RoomReservation> Reservations { get; init; }
}
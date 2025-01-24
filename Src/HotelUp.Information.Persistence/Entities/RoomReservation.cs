namespace HotelUp.Information.Persistence.Entities;

public class RoomReservation
{
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}
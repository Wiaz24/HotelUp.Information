namespace HotelUp.Information.Persistence.Entities;

public class RoomReservation
{
    public required Guid Id { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}
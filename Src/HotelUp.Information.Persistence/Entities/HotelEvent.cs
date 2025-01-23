namespace HotelUp.Information.Persistence.Entities;

public class HotelEvent
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
}
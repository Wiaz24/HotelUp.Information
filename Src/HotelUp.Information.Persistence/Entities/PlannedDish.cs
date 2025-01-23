namespace HotelUp.Information.Persistence.Entities;

public class PlannedDish
{
    public required string Name { get; init; }
    public required string ImageUrl { get; init; }
    public required DateOnly ServingDate { get; init; }
}
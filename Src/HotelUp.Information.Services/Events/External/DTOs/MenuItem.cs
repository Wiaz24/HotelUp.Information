namespace HotelUp.Information.Services.Events.External.DTOs;

public record MenuItem
{
    public required string Name { get; init; }
    public required string ImageUrl { get; init; }
}
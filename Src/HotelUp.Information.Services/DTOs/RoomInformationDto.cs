using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Services.DTOs;

public record RoomInformationDto
{
    public required int Number { get; init; }
    public required int Capacity { get; init; }
    public required bool WithSpecialNeeds { get; init; }
    
    public static RoomInformationDto FromEntity(RoomInformation roomInformation)
    {
        return new RoomInformationDto
        {
            Number = roomInformation.Number,
            Capacity = roomInformation.Capacity,
            WithSpecialNeeds = roomInformation.WithSpecialNeeds
        };
    }
}
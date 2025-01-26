using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Services.DTOs;

namespace HotelUp.Information.Services.Services;

public interface IRoomInformationService
{
    Task<IEnumerable<RoomInformationDto>> FindAvailableRoomsAsync(DateTime dateTime);
    Task GenerateExampleData();
    Task DeleteExampleData();
}
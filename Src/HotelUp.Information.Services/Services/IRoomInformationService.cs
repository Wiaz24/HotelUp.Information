using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Services.Services;

public interface IRoomInformationService
{
    Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime);
    Task GenerateExampleData();
    Task DeleteExampleData();
}
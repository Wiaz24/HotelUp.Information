using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Persistence.Repositories;

public interface IRoomInformationRepository
{
    Task<RoomInformation?> FindByRoomNumberAsync(int roomNumber);
    Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime);
    Task<RoomInformation?> GetAsync(int roomNumber);
    Task AddAsync(RoomInformation roomInformation);
    Task AddRangeAsync(IEnumerable<RoomInformation> roomInformationEntries);
    Task UpdateAsync(RoomInformation roomInformation);
    Task DeleteAsync(RoomInformation roomInformation);
}
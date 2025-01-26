using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Persistence.Repositories;

public interface IRoomInformationRepository
{
    Task<RoomInformation?> FindByRoomNumberAsync(int roomNumber);
    Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime);
    Task<RoomInformation?> GetAsync(int roomNumber);
    Task<IEnumerable<RoomInformation>> GetWithProvidedNumbersAsync(IEnumerable<int> roomNumbers);
    Task<IEnumerable<RoomInformation>> GetByReservationIdAsync(Guid reservationId);
    Task AddAsync(RoomInformation roomInformation);
    Task AddRangeAsync(IEnumerable<RoomInformation> roomInformationEntries);
    Task UpdateAsync(RoomInformation roomInformation);
    Task UpdateRangeAsync(IEnumerable<RoomInformation> roomInformationEntries);
    Task DeleteAsync(RoomInformation roomInformation);
}
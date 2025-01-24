using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Services.Services;

public interface IRoomInformationService
{
    Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime);
    Task GenerateExampleData();
    Task DeleteExampleData();
    Task AddRoomAsync(RoomInformation roomInformation);
    Task AttachReservationToRoomsAsync(Guid reservationId, 
        DateTime startDate, 
        DateTime endDate, 
        IEnumerable<int> roomNumbers);
    Task DetachReservationFromRoomsAsync(Guid reservationId);
}
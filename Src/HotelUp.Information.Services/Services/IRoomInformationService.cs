using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Services.DTOs;

namespace HotelUp.Information.Services.Services;

public interface IRoomInformationService
{
    Task<IEnumerable<RoomInformationDto>> FindAvailableRoomsAsync(DateTime dateTime);
    Task GenerateExampleData();
    Task DeleteExampleData();
    Task AddRoomAsync(RoomInformation roomInformation);
    Task AttachReservationToRoomsAsync(Guid reservationId, 
        DateTime startDate, 
        DateTime endDate, 
        IEnumerable<int> roomNumbers);
    Task DetachReservationFromRoomsAsync(Guid reservationId);
}
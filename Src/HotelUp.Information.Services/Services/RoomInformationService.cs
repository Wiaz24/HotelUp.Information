using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;

namespace HotelUp.Information.Services.Services;

public class RoomInformationService : IRoomInformationService
{
    private readonly IRoomInformationRepository _roomInformationRepository;

    public RoomInformationService(IRoomInformationRepository roomInformationRepository)
    {
        _roomInformationRepository = roomInformationRepository;
    }

    public Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime)
    {
        return _roomInformationRepository.FindAvailableRoomsAsync(dateTime);
    }

    public async Task GenerateExampleData()
    {
        var roomInformationEntries = new List<RoomInformation>()
        {
            new RoomInformation
            {
                Number = 101,
                Capacity = 2,
                Reservations = new()
                {
                    new RoomReservation()
                    {
                        StartDate = DateTime.UtcNow.Date,
                        EndDate = DateTime.UtcNow.Date.AddDays(1)
                    }
                },
                WithSpecialNeeds = false
            },
            new RoomInformation
            {
                Number = 102,
                Capacity = 1,
                Reservations = new()
                {
                    new RoomReservation()
                    {
                        StartDate = DateTime.UtcNow.Date.AddDays(1),
                        EndDate = DateTime.UtcNow.Date.AddDays(3)
                    }
                },
                WithSpecialNeeds = true
            }
        };
        await _roomInformationRepository.AddRangeAsync(roomInformationEntries);
    }

    public async Task DeleteExampleData()
    {
        var roomNumbers = new List<int> {101, 102};
        foreach (var roomNumber in roomNumbers)   
        {
            var roomInformation = await _roomInformationRepository.GetAsync(roomNumber);
            if (roomInformation != null)
            {
                await _roomInformationRepository.DeleteAsync(roomInformation);
            }
        }
    }
}
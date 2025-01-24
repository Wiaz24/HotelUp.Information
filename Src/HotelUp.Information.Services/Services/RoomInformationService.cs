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
                        ReservationId = Guid.NewGuid(),
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
                        ReservationId = Guid.NewGuid(),
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

    public Task AddRoomAsync(RoomInformation roomInformation)
    {
        return _roomInformationRepository.AddAsync(roomInformation);
    }

    public async Task AttachReservationToRoomsAsync(Guid reservationId, DateTime startDate, 
        DateTime endDate, IEnumerable<int> roomNumbers)
    {
        var reservation = new RoomReservation
        {
            ReservationId = reservationId,
            StartDate = startDate,
            EndDate = endDate
        };
        var rooms = (await _roomInformationRepository
            .GetWithProvidedNumbersAsync(roomNumbers))
            .ToList();
        foreach (var room in rooms)
        {
            room.Reservations.Add(reservation);
        }
        await _roomInformationRepository.UpdateRangeAsync(rooms);
    }

    public async Task DetachReservationFromRoomsAsync(Guid reservationId)
    {
        var roomInformationEntries = (await _roomInformationRepository
            .GetByReservationIdAsync(reservationId)).ToList();
        foreach (var entry in roomInformationEntries)
        {
            entry.Reservations.RemoveAll(x => x.ReservationId == reservationId);
        }
        await _roomInformationRepository.UpdateRangeAsync(roomInformationEntries);
    }
}
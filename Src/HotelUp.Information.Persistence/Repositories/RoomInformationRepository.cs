using HotelUp.Information.Persistence.EFCore;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelUp.Information.Persistence.Repositories;

public class RoomInformationRepository : IRoomInformationRepository
{
    private readonly DbSet<RoomInformation> _roomInformation;
    private readonly AppDbContext _dbContext;

    public RoomInformationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _roomInformation = dbContext.Set<RoomInformation>();
    }

    public Task<RoomInformation?> FindByRoomNumberAsync(int roomNumber)
    {
        return _roomInformation
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Number == roomNumber);
    }

    public async Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime)
    {
        return await _roomInformation
            .AsNoTracking()
            .Where(x => x.Reservations
                .All(r => r.StartDate > dateTime || r.EndDate < dateTime))
            .ToListAsync();
    }

    public Task<RoomInformation?> GetAsync(int roomNumber)
    {
        return _roomInformation
            .FirstOrDefaultAsync(x => x.Number == roomNumber);
    }

    public async Task<IEnumerable<RoomInformation>> GetWithProvidedNumbersAsync(IEnumerable<int> roomNumbers)
    {
        return await _roomInformation
            .Where(x => roomNumbers.Contains(x.Number))
            .ToListAsync();
    }

    public async Task<IEnumerable<RoomInformation>> GetByReservationIdAsync(Guid reservationId)
    {
        return await _roomInformation
            .Where(x => x.Reservations
                .Any(r => r.ReservationId == reservationId))
            .ToListAsync();
    }

    public async Task AddAsync(RoomInformation roomInformation)
    {
        await _roomInformation.AddAsync(roomInformation);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task AddRangeAsync(IEnumerable<RoomInformation> roomInformationEntries)
    {
        await _roomInformation.AddRangeAsync(roomInformationEntries);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RoomInformation roomInformation)
    {
        _roomInformation.Update(roomInformation);
        await _dbContext.SaveChangesAsync();
    }

    public Task UpdateRangeAsync(IEnumerable<RoomInformation> roomInformationEntries)
    {
        _roomInformation.UpdateRange(roomInformationEntries);
        return _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RoomInformation roomInformation)
    {
        _roomInformation.Remove(roomInformation);
        await _dbContext.SaveChangesAsync();
    }
}
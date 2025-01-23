using HotelUp.Information.Persistence.EF;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelUp.Information.Persistence.Repositories;

public class RoomInformationRepository : IRoomInformationRepository
{
    private readonly DbSet<RoomInformation> _roomInformation;
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public RoomInformationRepository(AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
        _roomInformation = dbContext.Set<RoomInformation>();
    }

    public async Task<RoomInformation?> FindByRoomNumberAsync(int roomNumber)
    {
        var cacheKey = $"RoomInformation_{roomNumber}";
        var cachedResult = _memoryCache.Get<RoomInformation?>(cacheKey);
        if (cachedResult is not null)
        {
            return cachedResult;
        }
        var result = await  _roomInformation
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Number == roomNumber);
        _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }

    public async Task<IEnumerable<RoomInformation>> FindAvailableRoomsAsync(DateTime dateTime)
    {
        var cacheKey = $"RoomInformation_{dateTime}";
        var cachedResult = _memoryCache.Get<IEnumerable<RoomInformation>>(cacheKey);
        if (cachedResult is not null)
        {
            return cachedResult;
        }
        var result = await  _roomInformation
            .AsNoTracking()
            .Where(x => x.Reservations
                .All(r => r.StartDate > dateTime || r.EndDate < dateTime))
            .ToListAsync();
        _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }

    public Task<RoomInformation?> GetAsync(int roomNumber)
    {
        return _roomInformation
            .FirstOrDefaultAsync(x => x.Number == roomNumber);
    }

    public async Task AddAsync(RoomInformation roomInformation)
    {
        await _roomInformation.AddAsync(roomInformation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RoomInformation roomInformation)
    {
        _roomInformation.Update(roomInformation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RoomInformation roomInformation)
    {
        _roomInformation.Remove(roomInformation);
        await _dbContext.SaveChangesAsync();
    }
}
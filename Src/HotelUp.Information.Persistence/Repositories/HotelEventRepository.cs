using HotelUp.Information.Persistence.EF;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelUp.Information.Persistence.Repositories;

public class HotelEventRepository : IHotelEventRepository
{
    private readonly DbSet<HotelEvent> _hotelEvents;
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public HotelEventRepository(AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
        _hotelEvents = dbContext.Set<HotelEvent>();
    }

    public async Task<HotelEvent?> FindByIdAsync(Guid id)
    {
        var cacheKey = $"HotelEvent_{id}";
        var cachedResult = _memoryCache.Get<HotelEvent?>(cacheKey);
        if (cachedResult is not null)
        {
            return cachedResult;
        }
        var result = await _hotelEvents
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }

    public async Task<IEnumerable<HotelEvent>> FindByDateAsync(DateOnly date)
    {
        var cacheKey = $"HotelEvent_{date}";
        var cachedResult = _memoryCache.Get<IEnumerable<HotelEvent>>(cacheKey);
        if (cachedResult is not null)
        {
            return cachedResult;
        }
        var dateValue = date.ToDateTime(TimeOnly.MinValue).Date;
        var result = await _hotelEvents
            .AsNoTracking()
            .Where(x => x.Date.Date == dateValue)
            .ToListAsync();
        _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }

    public Task<HotelEvent?> GetAsync(Guid id)
    {
        return _hotelEvents
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(HotelEvent hotelEvent)
    {
        await _hotelEvents.AddAsync(hotelEvent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(HotelEvent hotelEvent)
    {
        _hotelEvents.Update(hotelEvent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(HotelEvent hotelEvent)
    {
        _hotelEvents.Remove(hotelEvent);
        await _dbContext.SaveChangesAsync();
    }
}
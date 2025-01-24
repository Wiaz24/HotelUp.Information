using HotelUp.Information.Persistence.EFCore;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace HotelUp.Information.Persistence.Repositories;

public class HotelEventRepository : IHotelEventRepository
{
    private readonly DbSet<HotelEvent> _hotelEvents;
    private readonly AppDbContext _dbContext;
    public HotelEventRepository(AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _hotelEvents = dbContext.Set<HotelEvent>();
    }

    public Task<HotelEvent?> GetByIdAsync(Guid id)
    {
        return _hotelEvents
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<HotelEvent>> GetAllAsync()
    {
        return await _hotelEvents
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<HotelEvent>> GetByDateAsync(DateOnly date)
    {
        var dateValue = date.ToDateTime(TimeOnly.MinValue).Date;
        var result = await _hotelEvents
            .AsNoTracking()
            .Where(x => x.Date.Date == dateValue)
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<HotelEvent>> GetByTitleFragmentAsync(string searchString)
    {
        var result = await _hotelEvents
            .Where(x => EF.Functions.ILike(x.Title, $"%{searchString}%"))
            .ToListAsync();
        return result;
    }

    public async Task AddAsync(HotelEvent hotelEvent)
    {
        await _hotelEvents.AddAsync(hotelEvent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<HotelEvent> hotelEvents)
    {
        await _hotelEvents.AddRangeAsync(hotelEvents);
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
    

    public async Task DeleteRangeAsync(IEnumerable<HotelEvent> hotelEvents)
    {
        _hotelEvents.RemoveRange(hotelEvents);
        await _dbContext.SaveChangesAsync();
    }
}
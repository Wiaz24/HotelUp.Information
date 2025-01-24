using HotelUp.Information.Persistence.EFCore;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelUp.Information.Persistence.Repositories;

public class PlannedDishRepository : IPlannedDishRepository
{
    private readonly DbSet<PlannedDish> _plannedDishes;
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public PlannedDishRepository(AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _plannedDishes = dbContext.Set<PlannedDish>();
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }

    public async Task<PlannedDish?> GetByNameAsync(string name)
    {
        var cacheKey = $"PlannedDish_{name}";
        var cachedResult = _memoryCache.Get<PlannedDish?>(cacheKey);
        if (cachedResult is not null)
        {
            return cachedResult;
        }
        var result = await  _plannedDishes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
        _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }

    public async Task<IEnumerable<PlannedDish>> GetByServingDateAsync(DateOnly date)
    {
        var cacheKey = $"PlannedDish_{date}";
        var cachedResult = _memoryCache.Get<IEnumerable<PlannedDish>>(cacheKey);
        if (cachedResult is not null)
        {
            return cachedResult;
        }
        var result = await  _plannedDishes
            .AsNoTracking()
            .Where(x => x.ServingDate == date)
            .ToListAsync();
        _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }

    public async Task AddAsync(PlannedDish plannedDish)
    {
        await _plannedDishes.AddAsync(plannedDish);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(PlannedDish plannedDish)
    {
        _plannedDishes.Remove(plannedDish);
        await _dbContext.SaveChangesAsync();
    }
}
using HotelUp.Information.Persistence.EFCore;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelUp.Information.Persistence.Repositories;

public class PlannedDishRepository : IPlannedDishRepository
{
    private readonly DbSet<PlannedDish> _plannedDishes;
    private readonly AppDbContext _dbContext;

    public PlannedDishRepository(AppDbContext dbContext)
    {
        _plannedDishes = dbContext.Set<PlannedDish>();
        _dbContext = dbContext;
    }

    public Task<PlannedDish?> GetByNameAsync(string name)
    {
        return _plannedDishes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IEnumerable<PlannedDish>> GetByServingDateAsync(DateOnly date)
    {
        var result = await  _plannedDishes
            .AsNoTracking()
            .Where(x => x.ServingDate == date)
            .ToListAsync();
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
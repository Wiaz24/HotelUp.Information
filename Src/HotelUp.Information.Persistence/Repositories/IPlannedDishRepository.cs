using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Persistence.Repositories;

public interface IPlannedDishRepository
{
    Task<PlannedDish?> GetByNameAsync(string name);
    Task<IEnumerable<PlannedDish>> GetByServingDateAsync(DateOnly date);
    Task<IEnumerable<PlannedDish>> GetByNameFragmentAsync(string nameFragment);
    Task AddAsync(PlannedDish plannedDish);
    Task AddRangeAsync(IEnumerable<PlannedDish> plannedDishes);
    Task RemoveAsync(PlannedDish plannedDish);
    Task RemoveRangeAsync(IEnumerable<PlannedDish> plannedDishes);
}
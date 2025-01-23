using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Persistence.Repositories;

public interface IPlannedDishRepository
{
    Task<PlannedDish?> GetByNameAsync(string name);
    Task<IEnumerable<PlannedDish>> GetByServingDateAsync(DateOnly date);
    Task AddAsync(PlannedDish plannedDish);
    Task RemoveAsync(PlannedDish plannedDish);
}
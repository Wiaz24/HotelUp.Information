using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Services.Services;

public interface IPlannedDishService
{
    Task GenerateExampleData();
    Task DeleteExampleData();
    Task<IEnumerable<PlannedDish>> GetByDateAsync(DateOnly date);
}
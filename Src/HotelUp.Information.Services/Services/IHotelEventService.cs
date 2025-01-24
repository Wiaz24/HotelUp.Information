using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Services.Services;

public interface IHotelEventService
{
    Task GenerateExampleData();
    Task DeleteExampleData();
    Task<IEnumerable<HotelEvent>> GetByDateAsync(DateOnly date);
    Task<IEnumerable<HotelEvent>> GetAllAsync();
}
using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Persistence.Repositories;

public interface IHotelEventRepository
{
    Task<HotelEvent?> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelEvent>> GetAllAsync();
    Task<IEnumerable<HotelEvent>> GetByDateAsync(DateOnly date);
    Task<IEnumerable<HotelEvent>> GetByTitleFragmentAsync(string searchString);
    Task AddAsync(HotelEvent hotelEvent);
    Task AddRangeAsync(IEnumerable<HotelEvent> hotelEvents);
    Task UpdateAsync(HotelEvent hotelEvent);
    Task DeleteAsync(HotelEvent hotelEvent);
    Task DeleteRangeAsync(IEnumerable<HotelEvent> hotelEvents);
}
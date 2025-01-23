using HotelUp.Information.Persistence.Entities;

namespace HotelUp.Information.Persistence.Repositories;

public interface IHotelEventRepository
{
    Task<HotelEvent?> FindByIdAsync(Guid id);
    Task<IEnumerable<HotelEvent>> FindByDateAsync(DateOnly date);
    Task<HotelEvent?> GetAsync(Guid id);
    Task AddAsync(HotelEvent hotelEvent);
    Task UpdateAsync(HotelEvent hotelEvent);
    Task DeleteAsync(HotelEvent hotelEvent);
}
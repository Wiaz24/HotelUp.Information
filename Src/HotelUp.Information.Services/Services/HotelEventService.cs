using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;

namespace HotelUp.Information.Services.Services;

public class HotelEventService : IHotelEventService
{
    private readonly IHotelEventRepository _hotelEventRepository;

    public HotelEventService(IHotelEventRepository hotelEventRepository)
    {
        _hotelEventRepository = hotelEventRepository;
    }

    public async Task GenerateExampleData()
    {
        var exampleEvents = new List<HotelEvent>();
        for (int i = 0; i < 5; i++)
        {
            var hotelEvent = new HotelEvent
            {
                Id = Guid.NewGuid(),
                Title = $"Example Event number {i + 1}",
                Description = $"This is an example event number {i + 1}",
                Date = DateTime.Now.AddDays(i)
            };
            exampleEvents.Add(hotelEvent);
        }
        await _hotelEventRepository.AddRangeAsync(exampleEvents);
    }
    
    public async Task DeleteExampleData()
    {
        var exampleEvents = await _hotelEventRepository
            .GetByTitleFragmentAsync("Example Event number");
        await _hotelEventRepository.DeleteRangeAsync(exampleEvents);
    }

    public Task<IEnumerable<HotelEvent>> GetByDateAsync(DateOnly date)
    {
        return _hotelEventRepository.GetByDateAsync(date);
    }

    public Task<IEnumerable<HotelEvent>> GetAllAsync()
    {
        return _hotelEventRepository.GetAllAsync();
    }
}
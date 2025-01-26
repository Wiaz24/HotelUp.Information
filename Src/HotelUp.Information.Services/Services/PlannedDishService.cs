using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;

namespace HotelUp.Information.Services.Services;

public class PlannedDishService : IPlannedDishService
{
    private readonly IPlannedDishRepository _plannedDishRepository;
    private readonly TimeProvider _timeProvider;

    public PlannedDishService(IPlannedDishRepository plannedDishRepository, TimeProvider timeProvider)
    {
        _plannedDishRepository = plannedDishRepository;
        _timeProvider = timeProvider;
    }

    public async Task GenerateExampleData()
    {
        var exampleEvents = new List<PlannedDish>()
        {
            new PlannedDish()
            {
                Name = "Example Spaghetti bolognese",
                ServingDate = DateOnly.FromDateTime(_timeProvider.GetUtcNow().Date),
                ImageUrl = "https://thumbs.dreamstime.com/z/spaghetti-bolognese-black-serving-platter-fresh-basil-parmesan-44237851.jpg"
            },
            new PlannedDish()
            {
                Name = "Example Chicken curry",
                ServingDate = DateOnly.FromDateTime(_timeProvider.GetUtcNow().Date).AddDays(1),
                ImageUrl = "https://thumbs.dreamstime.com/z/bowl-delicious-creamy-butter-chicken-curry-basmati-rice-garlic-naan-bread-creamy-butter-chicken-curry-127026937.jpg"
            }, 
            new PlannedDish()
            {
                Name = "Example Pizza",
                ServingDate = DateOnly.FromDateTime(_timeProvider.GetUtcNow().Date).AddDays(2),
                ImageUrl = "https://thumbs.dreamstime.com/z/pizza-17450954.jpg"
            }
        };
        
        await _plannedDishRepository.AddRangeAsync(exampleEvents);
    }

    public async Task DeleteExampleData()
    {
        var exampleDishes = await _plannedDishRepository.GetByNameFragmentAsync("Example");
        await _plannedDishRepository.RemoveRangeAsync(exampleDishes);
    }

    public Task<IEnumerable<PlannedDish>> GetByDateAsync(DateOnly date)
    {
        return _plannedDishRepository.GetByServingDateAsync(date);
    }

    public Task AddDishesAsync(IEnumerable<PlannedDish> dishes)
    {
        return _plannedDishRepository.AddRangeAsync(dishes);
    }
}
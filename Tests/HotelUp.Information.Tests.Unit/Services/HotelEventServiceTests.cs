using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;
using HotelUp.Information.Services.Services;
using NSubstitute;

namespace HotelUp.Information.Tests.Unit.Services;

public class HotelEventServiceTests
{
    [Fact]
    public async Task GenerateExampleData_ShouldAddFiveEvents()
    {
        // Arrange
        var hotelEventRepository = Substitute.For<IHotelEventRepository>();
        var hotelEventService = new HotelEventService(hotelEventRepository);

        // Act
        await hotelEventService.GenerateExampleData();

        // Assert
        await hotelEventRepository
            .Received(1)
            .AddRangeAsync(Arg.Is<IEnumerable<HotelEvent>>(events => 
                events.Count() == 5));
    }
    
    [Fact]
    public async Task DeleteExampleData_ShouldDeleteAllExampleEvents()
    {
        // Arrange
        var hotelEventRepository = Substitute.For<IHotelEventRepository>();
        var hotelEventService = new HotelEventService(hotelEventRepository);
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
        hotelEventRepository.GetByTitleFragmentAsync("Example Event number")
            .Returns(exampleEvents);

        // Act
        await hotelEventService.DeleteExampleData();

        // Assert
        await hotelEventRepository
            .Received(1)
            .DeleteRangeAsync(Arg.Is<IEnumerable<HotelEvent>>(events => 
                events.Count() == 5));
    }
}
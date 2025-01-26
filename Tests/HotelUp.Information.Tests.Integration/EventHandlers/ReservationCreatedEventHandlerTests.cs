using HotelUp.Customer.Application.Events;
using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;
using HotelUp.Information.Services.Events.External.DTOs;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit.Abstractions;

namespace HotelUp.Information.Tests.Integration.EventHandlers;

[Collection(nameof(ReservationCreatedEventHandlerTests))]
public class ReservationCreatedEventHandlerTests : IntegrationTestsBase
{
    private readonly IBus _bus;

    public ReservationCreatedEventHandlerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper)
        : base(factory, testOutputHelper, "")
    {
        _bus = ServiceProvider.GetRequiredService<IBus>();
    }

    private async Task<IEnumerable<RoomInformation>> GetRoomInformationsAsync(Guid reservationId)
    {
        using var scope = ServiceProvider.CreateScope();
        var roomInformationRepository = scope.ServiceProvider.GetRequiredService<IRoomInformationRepository>();
        return await roomInformationRepository.GetByReservationIdAsync(reservationId);
    }

    private async Task<IEnumerable<RoomInformation>> CreateExampleRoomInformationsAsync(int count)
    {
        using var scope = ServiceProvider.CreateScope();
        var roomInformationRepository = scope.ServiceProvider.GetRequiredService<IRoomInformationRepository>();
        var roomInformations = new List<RoomInformation>();
        for (var i = 0; i < count; i++)
        {
            var roomInformation = new RoomInformation
            {
                Number = i + 1,
                Capacity = 1,
                WithSpecialNeeds = i % 2 == 0,
                Reservations = []
            };
            roomInformations.Add(roomInformation);
        }

        await roomInformationRepository.AddRangeAsync(roomInformations);
        return roomInformations;
    }

    [Fact]
    public async Task Consume_WhenCalled_ShouldAttachReservationToRooms()
    {
        await CreateExampleRoomInformationsAsync(3);
        var exampleImageUrl = "https://example.com/image.jpg";
        var reservationId = Guid.NewGuid();
        var reservationCreatedEvent = new ReservationCreatedEvent
        {
            ReservationId = reservationId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Rooms = new List<RoomDto>
            {
                new()
                {
                    Id = 1,
                    Capacity = 1,
                    Floor = 1,
                    ImageUrl = exampleImageUrl,
                    Type = RoomType.Basic,
                    WithSpecialNeeds = false
                }
            }
        };

        (await GetRoomInformationsAsync(reservationId)).ShouldBeEmpty();

        await _bus.Publish(reservationCreatedEvent);

        await Task.Delay(500);

        var roomInformations = (await GetRoomInformationsAsync(reservationId)).ToList();
        roomInformations.ShouldNotBeEmpty();
        roomInformations.Count().ShouldBe(1);
        roomInformations[0].Number.ShouldBe(1);
    }
}
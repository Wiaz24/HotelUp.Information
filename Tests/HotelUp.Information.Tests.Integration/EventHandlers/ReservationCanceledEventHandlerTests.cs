using HotelUp.Customer.Application.Events;
using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Persistence.Repositories;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit.Abstractions;

namespace HotelUp.Information.Tests.Integration.EventHandlers;

[Collection(nameof(ReservationCanceledEventHandlerTests))]
public class ReservationCanceledEventHandlerTests : IntegrationTestsBase
{
    private readonly IBus _bus;

    public ReservationCanceledEventHandlerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper)
        : base(factory, testOutputHelper, "")
    {
        _bus = ServiceProvider.GetRequiredService<IBus>();
    }

    private async Task<List<RoomInformation>> GetRoomInformationsAsync(Guid reservationId)
    {
        using var scope = ServiceProvider.CreateScope();
        var roomInformationRepository = scope.ServiceProvider.GetRequiredService<IRoomInformationRepository>();
        return (await roomInformationRepository.GetByReservationIdAsync(reservationId)).ToList();
    }

    private async Task<List<RoomInformation>> CreateExampleRoomInformationsAsync(int count)
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

    private async Task AttachExampleReservationToRoomsAsync(Guid reservationId, IEnumerable<int> roomNumbers)
    {
        using var scope = ServiceProvider.CreateScope();
        var roomInformationRepository = scope.ServiceProvider.GetRequiredService<IRoomInformationRepository>();
        var roomInformations = (await roomInformationRepository
            .GetWithProvidedNumbersAsync(roomNumbers)).ToList();
        foreach (var roomInformation in roomInformations)
            roomInformation.Reservations.Add(new RoomReservation
            {
                ReservationId = reservationId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            });
        await roomInformationRepository.UpdateRangeAsync(roomInformations);
    }

    [Fact]
    public async Task Consume_WhenCalled_ShouldDetachReservationFromRooms()
    {
        var roomInformations = await CreateExampleRoomInformationsAsync(3);
        var reservationId = Guid.NewGuid();
        var otherReservationId = Guid.NewGuid();
        await AttachExampleReservationToRoomsAsync(reservationId, [1, 2]);
        await AttachExampleReservationToRoomsAsync(otherReservationId, [3]);

        var roomInformationsBefore = await GetRoomInformationsAsync(reservationId);
        roomInformationsBefore.ShouldNotBeEmpty();
        roomInformationsBefore.Count.ShouldBe(2);

        var reservationCanceledEvent = new ReservationCanceledEvent
        {
            ReservationId = reservationId
        };

        await _bus.Publish(reservationCanceledEvent);

        await Task.Delay(100);

        var roomInformationsAfter = await GetRoomInformationsAsync(reservationId);
        roomInformationsAfter.ShouldBeEmpty();
        var otherRoomInformations = await GetRoomInformationsAsync(otherReservationId);
        otherRoomInformations.ShouldNotBeEmpty();
        otherRoomInformations.Count.ShouldBe(1);
    }
}
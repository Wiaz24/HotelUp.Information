using HotelUp.Customer.Application.Events;
using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Services.Services;
using MassTransit;

namespace HotelUp.Information.Services.Events.External.Handlers;

public class RoomCreatedEventHandler : IConsumer<RoomCreatedEvent>
{
    private readonly IRoomInformationService _roomInformationService;

    public RoomCreatedEventHandler(IRoomInformationService roomInformationService)
    {
        _roomInformationService = roomInformationService;
    }

    public async Task Consume(ConsumeContext<RoomCreatedEvent> context)
    {
        var message = context.Message;
        var roomInformation = new RoomInformation
        {
            Number = message.Id,
            Capacity = message.Capacity,
            WithSpecialNeeds = message.WithSpecialNeeds,
            Reservations = []
        };
        await _roomInformationService.AddRoomAsync(roomInformation);
    }
}
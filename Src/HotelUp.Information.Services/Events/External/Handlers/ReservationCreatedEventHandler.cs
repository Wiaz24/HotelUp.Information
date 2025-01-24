using HotelUp.Customer.Application.Events;
using HotelUp.Information.Services.Services;
using MassTransit;

namespace HotelUp.Information.Services.Events.External.Handlers;

public class ReservationCreatedEventHandler : IConsumer<ReservationCreatedEvent>
{
    private readonly IRoomInformationService _roomInformationService;

    public ReservationCreatedEventHandler(IRoomInformationService roomInformationService)
    {
        _roomInformationService = roomInformationService;
    }

    public async Task Consume(ConsumeContext<ReservationCreatedEvent> context)
    {
        var message = context.Message;
        await _roomInformationService.AttachReservationToRoomsAsync(
            message.ReservationId,
            message.StartDate, 
            message.EndDate, 
            message.Rooms.Select(r => r.Id));
    }
}
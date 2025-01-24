using HotelUp.Customer.Application.Events;
using HotelUp.Information.Services.Services;
using MassTransit;

namespace HotelUp.Information.Services.Events.External.Handlers;

public class ReservationCanceledEventHandler : IConsumer<ReservationCanceledEvent>
{
    private readonly IRoomInformationService _roomInformationService;

    public ReservationCanceledEventHandler(IRoomInformationService roomInformationService)
    {
        _roomInformationService = roomInformationService;
    }

    public async Task Consume(ConsumeContext<ReservationCanceledEvent> context)
    {
        var message = context.Message;
        await _roomInformationService.DetachReservationFromRoomsAsync(message.ReservationId);
    }
}
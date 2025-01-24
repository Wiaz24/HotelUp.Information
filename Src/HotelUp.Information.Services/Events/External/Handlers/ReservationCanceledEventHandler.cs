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

    public Task Consume(ConsumeContext<ReservationCanceledEvent> context)
    {
        throw new NotImplementedException();
    }
}
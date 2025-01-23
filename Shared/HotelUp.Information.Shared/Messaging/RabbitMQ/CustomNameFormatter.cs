using MassTransit;

namespace HotelUp.Information.Shared.Messaging.RabbitMQ;

public class CustomNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return $"HotelUp.Information:{typeof(T).Name}";
    }
}
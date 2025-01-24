using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Information.API.DTOs;

public record GetHotelEventsDto
{
    [SwaggerParameter("The date of the event. If not provided, date is today. Example: 2022-12-31")]
    public DateOnly? Date { get; init; }
}
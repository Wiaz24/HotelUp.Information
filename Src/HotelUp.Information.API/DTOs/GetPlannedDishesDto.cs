using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Information.API.DTOs;

public record GetPlannedDishesDto
{
    [SwaggerParameter("The date of serving. If not provided, date is today. Example: 2022-12-31")]
    public DateOnly? Date { get; init; }
}
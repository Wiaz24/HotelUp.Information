using System.ComponentModel;

namespace HotelUp.Information.API.DTOs;

public record GetHotelEventsDto
{
    [DefaultValue(typeof(DateOnly), "2025-01-01")]
    public DateOnly? Date { get; init; }
}
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Information.API.DTOs;

public record GetAvailableRoomsDto
{
    [SwaggerParameter(@"The date and time for which to find available rooms.  
        If no dateTime is provider, returns currently available rooms. Example: 2022-12-31T23:59:59")]
    public DateTime? DateTime { get; init; }
}
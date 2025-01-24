using HotelUp.Information.API.DTOs;
using HotelUp.Information.Persistence.Entities;
using HotelUp.Information.Services.Services;
using HotelUp.Information.Shared.Auth;
using HotelUp.Information.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Information.API.Controllers;

[ApiController]
[Route("api/information/room-information")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public class RoomInformationController : ControllerBase
{
    private readonly IRoomInformationService _roomInformationService;
    private readonly TimeProvider _timeProvider;

    public RoomInformationController(IRoomInformationService roomInformationService, TimeProvider timeProvider)
    {
        _roomInformationService = roomInformationService;
        _timeProvider = timeProvider;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation("Returns available rooms by date. If date is not provided, returns currently available rooms.")]
    public async Task<ActionResult<IEnumerable<RoomInformation>>> GetAvailableRooms([FromQuery] GetAvailableRoomsDto dto)
    {
        var date = dto.DateTime ?? _timeProvider.GetUtcNow().DateTime;
        var availableRooms = await _roomInformationService.FindAvailableRoomsAsync(date);
        return Ok(availableRooms);
    }

    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpPost("example-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Generates example data for room information. (Requires admin role)")]
    public async Task<IActionResult> GenerateExampleData()
    {
        await _roomInformationService.GenerateExampleData();
        return Ok();
    }

    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpDelete("example-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Deletes example data for room information. (Requires admin role)")]
    public async Task<IActionResult> DeleteExampleData()
    {
        await _roomInformationService.DeleteExampleData();
        return Ok();
    }
    
}
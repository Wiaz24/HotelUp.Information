using System.Security.Claims;
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
[Route("api/information/hotel-event")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public class HotelEventController : ControllerBase
{
    private readonly TimeProvider _timeProvider;
    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)
        is { } id
        ? new Guid(id)
        : throw new TokenException("No user id found in access token.");
    
    private readonly IHotelEventService _hotelEventService;

    public HotelEventController(IHotelEventService hotelEventService, TimeProvider timeProvider)
    {
        _hotelEventService = hotelEventService;
        _timeProvider = timeProvider;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation("Returns hotel events by date. If date is not provided, returns events for today.")]
    public async Task<ActionResult<IEnumerable<HotelEvent>>> GetHotelEventsByDate([FromQuery] GetHotelEventsDto dto)
    {
        var date = dto.Date ?? DateOnly.FromDateTime(_timeProvider.GetUtcNow().Date);
        var hotelEvents = await _hotelEventService.GetByDateAsync(date);
        return Ok(hotelEvents);
    }

    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpPost("example-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Generates example data for hotel event. (Requires admin role)")]
    public async Task<IActionResult> GenerateExampleData()
    {
        await _hotelEventService.GenerateExampleData();
        return Ok();
    }
    
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpDelete("example-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Deletes example data for hotel event. (Requires admin role)")]
    public async Task<IActionResult> DeleteExampleData()
    {
        await _hotelEventService.DeleteExampleData();
        return Ok();
    }
    
    
}
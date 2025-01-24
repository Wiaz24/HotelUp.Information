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
[Route("api/information/planned-dish")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public class PlannedDishController : ControllerBase
{
    private readonly TimeProvider _timeProvider;
    private readonly IPlannedDishService _plannedDishService;

    public PlannedDishController(TimeProvider timeProvider, IPlannedDishService plannedDishService)
    {
        _timeProvider = timeProvider;
        _plannedDishService = plannedDishService;
    }

    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)
        is { } id
        ? new Guid(id)
        : throw new TokenException("No user id found in access token.");
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation("Returns planned dishes by date. If date is not provided, returns dishes for today.")]
    public async Task<ActionResult<IEnumerable<PlannedDish>>> GetPlannedDishesByDate([FromQuery] GetPlannedDishesDto dto)
    {
        var date = dto.Date ?? DateOnly.FromDateTime(_timeProvider.GetUtcNow().Date);
        var plannedDishes = await _plannedDishService.GetByDateAsync(date);
        return Ok(plannedDishes);
    }
    
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpPost("example-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Generates example data for planned dishes. (Requires admin role)")]
    public async Task<IActionResult> GenerateExampleData()
    {
        await _plannedDishService.GenerateExampleData();
        return Ok();
    }
    
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpDelete("example-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Deletes example data for planned dishes. (Requires admin role)")]
    public async Task<IActionResult> DeleteExampleData()
    {
        await _plannedDishService.DeleteExampleData();
        return Ok();
    }
}
using Microsoft.AspNetCore.Mvc;
using RedSignal.Models;
using RedSignal.Services;

namespace RedSignal.Controllers;

[ApiController]
[Route("api/v1/users/{userId}/monitored-locations")]
public class MonitoredLocationsController : ControllerBase
{
    private readonly IMonitoredLocationService _service;

    public MonitoredLocationsController(IMonitoredLocationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocalMonitorado>>> GetAll(long userId)
    {
        var result = await _service.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{locationId}")]
    public async Task<ActionResult<LocalMonitorado>> Get(long userId, long locationId)
    {
        var local = await _service.GetByIdAsync(userId, locationId);
        if (local == null) return NotFound();
        return Ok(local);
    }

    [HttpPost]
    public async Task<ActionResult<LocalMonitorado>> Create(long userId, [FromBody] LocalMonitorado local)
    {
        var created = await _service.CreateAsync(userId, local);
        return CreatedAtAction(nameof(Get), new { userId = userId, locationId = created.Id }, created);
    }

    [HttpPut("{locationId}")]
    public async Task<ActionResult<LocalMonitorado>> Update(long userId, long locationId, [FromBody] LocalMonitorado updated)
    {
        var result = await _service.UpdateAsync(userId, locationId, updated);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{locationId}")]
    public async Task<IActionResult> Delete(long userId, long locationId)
    {
        var deleted = await _service.DeleteAsync(userId, locationId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}

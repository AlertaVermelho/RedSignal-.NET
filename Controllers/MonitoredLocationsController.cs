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
    public async Task<IActionResult> GetAll(long userId)
    {
        var locais = await _service.GetAllByUserIdAsync(userId);
        return Ok(locais);
    }

    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetById(long userId, long locationId)
    {
        var local = await _service.GetByIdAsync(userId, locationId);
        return local == null ? NotFound() : Ok(local);
    }

    [HttpPost]
    public async Task<IActionResult> Create(long userId, [FromBody] LocalMonitorado local)
    {
        var created = await _service.CreateAsync(userId, local);
        return CreatedAtAction(nameof(GetById), new { userId = created.UserId, locationId = created.Id }, created);
    }

    [HttpPut("{locationId}")]
    public async Task<IActionResult> Update(long userId, long locationId, [FromBody] LocalMonitorado local)
    {
        var updated = await _service.UpdateAsync(userId, locationId, local);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{locationId}")]
    public async Task<IActionResult> Delete(long userId, long locationId)
    {
        var success = await _service.DeleteAsync(userId, locationId);
        return success ? NoContent() : NotFound();
    }
}

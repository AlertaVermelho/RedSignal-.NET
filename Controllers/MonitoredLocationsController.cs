using Microsoft.AspNetCore.Mvc;
using RedSignal.Models;
using RedSignal.Models.DTOs;
using RedSignal.Services;

namespace RedSignal.Controllers;

[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
[Route("api/v1/users/{userId}/monitored-locations")]
[Tags("Locais Monitorados")]
public class MonitoredLocationsController : ControllerBase
{
    private readonly IMonitoredLocationService _service;

    public MonitoredLocationsController(IMonitoredLocationService service)
    {
        _service = service;
    }

    /// <summary>Cadastra um novo local monitorado para o usuário</summary>
    /// <param name="userId">ID do usuário</param>
    /// <param name="dto">Dados do local monitorado</param>
    [HttpPost]
    public async Task<IActionResult> Create(long userId, [FromBody] LocalMonitoradoCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var local = new LocalMonitorado
        {
            NomeLocal = dto.NomeLocal,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            RaioNotificacaoKm = dto.RaioNotificacaoKm,
            UserId = userId,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        var created = await _service.CreateAsync(userId, local);
        return CreatedAtAction(nameof(GetById), new { userId = created.UserId, locationId = created.Id }, created);
    }

    /// <summary>Lista todos os locais monitorados de um usuário</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(long userId)
    {
        var locais = await _service.GetAllByUserIdAsync(userId);
        return Ok(locais);
    }

    /// <summary>Busca um local monitorado específico por ID</summary>
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetById(long userId, long locationId)
    {
        var local = await _service.GetByIdAsync(userId, locationId);
        return local == null ? NotFound() : Ok(local);
    }

    /// <summary>Atualiza os dados de um local monitorado</summary>
    [HttpPut("{locationId}")]
    public async Task<IActionResult> Update(long userId, long locationId, [FromBody] LocalMonitorado local)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(userId, locationId, local);
        return updated == null ? NotFound() : Ok(updated);
    }

    /// <summary>Remove um local monitorado</summary>
    [HttpDelete("{locationId}")]
    public async Task<IActionResult> Delete(long userId, long locationId)
    {
        var success = await _service.DeleteAsync(userId, locationId);
        return success ? NoContent() : NotFound();
    }

    /// <summary>Retorna todos os locais monitorados ativos</summary>
    /// <remarks>Endpoint interno usado pela API Java. Requer header X-Internal-Api-Key</remarks>
    [HttpGet("/api/v1/internal/monitored-locations/all-active")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Integração com API Java")]
    public async Task<IActionResult> GetAllActive([FromHeader(Name = "X-Internal-Api-Key")] string apiKey)
    {
        if (apiKey != "SUA_CHAVE_INTERNA")
            return Unauthorized();

        var locais = await _service.GetAllAtivosAsync();
        return Ok(locais);
    }
}

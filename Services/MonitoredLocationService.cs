using Microsoft.EntityFrameworkCore;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Services;

public class MonitoredLocationService : IMonitoredLocationService
{
    private readonly AppDbContext _context;

    public MonitoredLocationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LocalMonitorado>> GetAllByUserIdAsync(long userId)
    {
        return await _context.LocaisMonitorados
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<LocalMonitorado?> GetByIdAsync(long userId, long locationId)
    {
        return await _context.LocaisMonitorados
            .FirstOrDefaultAsync(x => x.Id == locationId && x.UserId == userId);
    }

    public async Task<LocalMonitorado> CreateAsync(long userId, LocalMonitorado local)
    {
        local.UserId = userId;
        local.DataCriacao = DateTime.UtcNow;
        local.DataAtualizacao = DateTime.UtcNow;

        _context.LocaisMonitorados.Add(local);
        await _context.SaveChangesAsync();
        return local;
    }

    public async Task<LocalMonitorado?> UpdateAsync(long userId, long locationId, LocalMonitorado updated)
    {
        var local = await GetByIdAsync(userId, locationId);
        if (local == null) return null;

        local.NomeLocal = updated.NomeLocal;
        local.Latitude = updated.Latitude;
        local.Longitude = updated.Longitude;
        local.RaioNotificacaoKm = updated.RaioNotificacaoKm;
        local.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return local;
    }

    public async Task<bool> DeleteAsync(long userId, long locationId)
    {
        var local = await GetByIdAsync(userId, locationId);
        if (local == null) return false;

        _context.LocaisMonitorados.Remove(local);
        await _context.SaveChangesAsync();
        return true;
    }
}

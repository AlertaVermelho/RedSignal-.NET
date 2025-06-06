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
            .Where(x => x.id_usuario_registrou == userId)
            .ToListAsync();
    }

    public async Task<LocalMonitorado?> GetByIdAsync(long userId, long locationId)
    {
        return await _context.LocaisMonitorados
            .FirstOrDefaultAsync(x => x.id_local_monitorado == locationId && x.id_usuario_registrou == userId);
    }

    public async Task<LocalMonitorado> CreateAsync(long userId, LocalMonitorado local)
    {
        local.id_usuario_registrou = userId;
        local.data_criacao = DateTime.UtcNow;
        local.data_atualizacao = DateTime.UtcNow;

        _context.LocaisMonitorados.Add(local);
        await _context.SaveChangesAsync();
        return local;
    }

    public async Task<LocalMonitorado?> UpdateAsync(long userId, long locationId, LocalMonitorado updated)
    {
        var local = await GetByIdAsync(userId, locationId);
        if (local == null) return null;

        local.nome_local = updated.nome_local;
        local.latitude = updated.latitude;
        local.longitude = updated.longitude;
        local.raio_notificacao_km = updated.raio_notificacao_km;
        local.data_atualizacao = DateTime.UtcNow;

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

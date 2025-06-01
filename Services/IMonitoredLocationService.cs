using RedSignal.Models;

namespace RedSignal.Services;

public interface IMonitoredLocationService
{
    Task<List<LocalMonitorado>> GetAllByUserIdAsync(long userId);
    Task<LocalMonitorado?> GetByIdAsync(long userId, long locationId);
    Task<LocalMonitorado> CreateAsync(long userId, LocalMonitorado local);
    Task<LocalMonitorado?> UpdateAsync(long userId, long locationId, LocalMonitorado updated);
    Task<bool> DeleteAsync(long userId, long locationId);
}

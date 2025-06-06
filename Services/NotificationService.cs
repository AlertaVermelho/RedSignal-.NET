using Microsoft.EntityFrameworkCore;
using RedSignal.Data;
using RedSignal.Models;

namespace RedSignal.Services;

public class NotificationService : INotificationService
{
    private readonly AppDbContext _context;

    public NotificationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificacaoDto>> GetNotificationsForUserAsync(long userId)
    {
        var notificacoes = new List<NotificacaoDto>();

        var locaisUsuario = await _context.LocaisMonitorados
            .AsNoTracking()
            .Where(l => l.UserId == userId)
            .ToListAsync();

        if (!locaisUsuario.Any())
        {
            return notificacoes;
        }

        var hotspotsAtivos = await _context.HotspotsEventos
            .AsNoTracking()
            .Where(h => h.status_hotspot == "ATIVO")
            .ToListAsync();

        if (!hotspotsAtivos.Any())
        {
            return notificacoes;
        }
        
        foreach (var local in locaisUsuario)
        {
            foreach (var hotspot in hotspotsAtivos)
            {
                var distancia = CalcularDistanciaHaversine(
                    local.Latitude, local.Longitude,
                    hotspot.centroide_latitude, hotspot.centroide_longitude
                );

                var raioDeAlertaTotal = local.RaioNotificacaoKm + (hotspot.raio_estimado_km ?? 0);

                if (distancia <= raioDeAlertaTotal)
                {
                    notificacoes.Add(new NotificacaoDto
                    {
                        LocalMonitorado = local,
                        HotspotProximo = hotspot,
                        DistanciaKm = Math.Round(distancia, 2)
                    });
                }
            }
        }

        return notificacoes;
    }

    private double CalcularDistanciaHaversine(double lat1, double lon1, double lat2, double lon2)
    {
        const double RaioTerraKm = 6371.0;

        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var lat1Rad = ToRadians(lat1);
        var lat2Rad = ToRadians(lat2);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return RaioTerraKm * c;
    }

    private double ToRadians(double angulo)
    {
        return Math.PI * angulo / 180.0;
    }
}

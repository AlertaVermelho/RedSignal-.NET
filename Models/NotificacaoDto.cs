using System.Text.Json.Serialization;

namespace RedSignal.Models;

public class NotificacaoDto
{
    [JsonPropertyName("localMonitorado")]
    public LocalMonitorado? LocalMonitorado { get; set; }

    [JsonPropertyName("hotspotProximo")]
    public HotspotEvento? HotspotProximo { get; set; }

    [JsonPropertyName("distanciaKm")]
    public double DistanciaKm { get; set; }
}

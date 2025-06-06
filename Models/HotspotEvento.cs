using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSignal.Models;

[Table("HOTSPOTS_EVENTOS")]
public class HotspotEvento
{
    [Key]
    [Column("id_hotspot")]
    public long id_hotspot { get; set; }

    [Column("centroide_latitude")]
    public double centroide_latitude { get; set; }

    [Column("centroide_longitude")]
    public double centroide_longitude { get; set; }

    [Column("raio_estimado_km")]
    public double? raio_estimado_km { get; set; }

    [Column("severidade_predominante")]
    public string severidade_predominante { get; set; } = string.Empty;

    [Column("tipo_predominante")]
    public string tipo_predominante { get; set; } = string.Empty;

    [Column("resumo_publico")]
    public string? resumo_publico { get; set; }

    [Column("timestamp_ultima_atividade")]
    public DateTime timestamp_ultima_atividade { get; set; }

    [Column("status_hotspot")]
    public string status_hotspot { get; set; } = string.Empty;
}

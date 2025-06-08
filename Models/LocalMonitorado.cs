using System.ComponentModel.DataAnnotations;

namespace RedSignal.Models;

public class LocalMonitorado
{
    public long Id { get; set; }

    [Required]
    public string NomeLocal { get; set; } = string.Empty;

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

    [Required]
    [Range(0.1, 999.9)]
    public double RaioNotificacaoKm { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

    // Chave estrangeira
    public long UserId { get; set; }
    public Usuario? Usuario { get; set; }
}

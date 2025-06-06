using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RedSignal.Models;

[Table("LOCAIS_MONITORADOS")]
public class LocalMonitorado
{
    [Key]
    [Column("id_local_monitorado")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [Required]
    [Column("nome_local")]
    [JsonPropertyName("nomeLocal")]
    public string NomeLocal { get; set; } = string.Empty;

    [Required]
    [Column("latitude")]
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [Required]
    [Column("longitude")]
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [Required]
    [Range(0.1, 999.9)]
    [Column("raio_notificacao_km")]
    [JsonPropertyName("raioNotificacaoKm")]
    public double RaioNotificacaoKm { get; set; }

    [Column("data_criacao")]
    [JsonPropertyName("dataCriacao")]
    public DateTime DataCriacao { get; set; }

    [Column("data_atualizacao")]
    [JsonPropertyName("dataAtualizacao")]
    public DateTime DataAtualizacao { get; set; }

    [Column("id_usuario_registrou")]
    [JsonPropertyName("userId")]
    public long UserId { get; set; }

    [JsonIgnore]
    public Usuario? Usuario { get; set; }
}

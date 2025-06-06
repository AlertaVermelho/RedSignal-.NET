using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSignal.Models;

[Table("LOCAIS_MONITORADOS")]
public class LocalMonitorado
{
    [Key]
    [Column("id_local_monitorado")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id_local_monitorado { get; set; }

    [Required]
    [Column("nome_local")]
    public string nome_local { get; set; } = string.Empty;

    [Required]
    [Column("latitude")]
    public double latitude { get; set; }

    [Required]
    [Column("longitude")]
    public double longitude { get; set; }

    [Required]
    [Range(0.1, 999.9)]
    [Column("raio_notificacao_km")]
    public double raio_notificacao_km { get; set; }

    [Column("data_criacao")]
    public DateTime data_criacao { get; set; } = DateTime.UtcNow;

    [Column("data_atualizacao")]
    public DateTime data_atualizacao { get; set; } = DateTime.UtcNow;

    // Chave estrangeira
    [Column("id_usuario_registrou")]
    public long id_usuario_registrou { get; set; }
    public Usuario? Usuario { get; set; }
}

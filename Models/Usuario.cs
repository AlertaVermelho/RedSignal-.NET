using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSignal.Models;

[Table("USUARIOS")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public long id_usuario { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    // Relacionamento 1:N
    public List<LocalMonitorado> LocaisMonitorados { get; set; } = new();
}

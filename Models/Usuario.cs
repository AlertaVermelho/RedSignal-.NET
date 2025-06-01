namespace RedSignal.Models;

public class Usuario
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Relacionamento 1:N
    public List<LocalMonitorado> LocaisMonitorados { get; set; } = new();
}

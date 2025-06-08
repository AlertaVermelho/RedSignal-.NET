namespace RedSignal.Models.DTOs;

public class LocalMonitoradoCreateDto
{
    public string NomeLocal { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double RaioNotificacaoKm { get; set; }
}

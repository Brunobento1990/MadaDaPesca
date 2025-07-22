namespace MadaDaPesca.Infra.HttpClient.DTOs;

internal class ClimaDTO
{
    public ClimaDetalhes Hourly { get; set; } = null!;
    public ClimaUnidadeDeMedidaDTO Hourly_units { get; set; } = null!;
}

internal class ClimaDetalhes
{
    public IEnumerable<DateTime> Time { get; set; } = [];
    public IEnumerable<double> Rain { get; set; } = [];
    public IEnumerable<double> Wind_speed_180m { get; set; } = [];
    public IEnumerable<double> Wind_direction_180m { get; set; } = [];
    public IEnumerable<double> Temperature_180m { get; set; } = [];
    public IEnumerable<double> Surface_pressure { get; set; } = [];
}

internal class ClimaUnidadeDeMedidaDTO
{
    public string Rain { get; set; } = string.Empty;
    public string Wind_speed_180m { get; set; } = string.Empty;
    public string Wind_direction_180m { get; set; } = string.Empty;
    public string Temperature_180m { get; set; } = string.Empty;
    public string Surface_pressure { get; set; } = string.Empty;
}

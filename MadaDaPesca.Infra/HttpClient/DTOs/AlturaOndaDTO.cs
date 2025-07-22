namespace MadaDaPesca.Infra.HttpClient.DTOs;

internal class AlturaOndaDTO
{
    public AlturaOndaDetalhes Hourly { get; set; } = null!;
}

internal class AlturaOndaDetalhes
{
    public IEnumerable<DateTime> Time { get; set; } = [];
    public IEnumerable<double> Wave_height { get; set; } = [];
    public IEnumerable<double> Sea_surface_temperature { get; set; } = [];
    public IEnumerable<double> Sea_level_height_msl { get; set; } = [];
}

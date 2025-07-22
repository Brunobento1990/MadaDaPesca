namespace MadaDaPesca.Application.ViewModel;

public class ClimaViewModel
{
    public IEnumerable<DateTime> Dias { get; set; } = [];
    public IEnumerable<double> Chuva { get; set; } = [];
    public IEnumerable<double> VelocidadeDoVento { get; set; } = [];
    public IEnumerable<double> DirecaoDoVento { get; set; } = [];
    public IEnumerable<double> Temperaturas { get; set; } = [];
    public IEnumerable<double> PressaoAtmosferica { get; set; } = [];
    public UnidadeDeMedidaClimaViewModel UnidadeDeMedida { get; set; } = null!;
    public int PrevisaoDeDias { get; set; }
}

public class UnidadeDeMedidaClimaViewModel
{
    public string Temperatura { get; set; } = string.Empty;
    public string Chuva { get; set; } = string.Empty;
    public string VelocidadeDoVento { get; set; } = string.Empty;
    public string DirecaoDoVento { get; set; } = string.Empty;
    public string PressaoAtmosferica { get; set; } = string.Empty;
}

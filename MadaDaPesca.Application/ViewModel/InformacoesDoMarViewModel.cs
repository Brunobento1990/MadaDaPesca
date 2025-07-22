namespace MadaDaPesca.Application.ViewModel;

public class InformacoesDoMarViewModel
{
    public IEnumerable<DateTime> Dias { get; set; } = [];
    public IEnumerable<double> AlturasDasOndas { get; set; } = [];
    public IEnumerable<double> TemperaturasDoMar { get; set; } = [];
    public IEnumerable<double> AlturasDaMare { get; set; } = [];
    public int PrevisaoDeDias { get; set; }
}

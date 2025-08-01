namespace MadaDaPesca.Application.ViewModel;

public class FaturaHomeViewModel
{
    public int Mes { get; set; }
    public int Ano { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal? ValorAReceber { get; set; }
    public decimal? ValorRecebido { get; set; }
}

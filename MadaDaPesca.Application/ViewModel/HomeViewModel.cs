using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Application.ViewModel;

public class HomeViewModel
{
    public InformacoesDoMarViewModel InformacoesDoMar { get; set; } = null!;
    public ClimaViewModel Clima { get; set; } = null!;
    public IEnumerable<AgendaPescariaViewModel> AgendaDeHoje { get; set; } = [];
    public IEnumerable<AgendaPescariaViewModel> AgendaDeAmanha { get; set; } = [];
    public FaturaHomeViewModel? Fatura { get; set; }
    public IEnumerable<FaturaHomeModel> Transacoes { get; set; } = [];
}

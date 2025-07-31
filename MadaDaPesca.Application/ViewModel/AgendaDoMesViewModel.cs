namespace MadaDaPesca.Application.ViewModel;

public class AgendaDoMesViewModel
{
    public IEnumerable<AgendaPescariaViewModel> Agenda { get; set; } = [];
    public IEnumerable<BloqueioDataPescariaViewModel> AgendaBloqueada { get; set; } = [];
}

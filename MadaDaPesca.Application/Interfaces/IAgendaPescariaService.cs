using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IAgendaPescariaService
{
    Task<AgendaPescariaViewModel> ObterPorIdAsync(Guid id);
    Task ExcluirAsync(Guid id);
    Task<AgendaPescariaViewModel> AgendarAsync(AgendarPescariaDTO agendarPescariaDTO);
    Task<AgendaPescariaViewModel> EditarAsync(EditarAgendaPescariaDTO editarAgendaPescariaDTO);
    Task<AgendaDoMesViewModel> AgendaDoMesAsync(short mes, short ano);
    Task<AgendaPescariaViewModel> ReagendarAsync(ReagendarDTO reagendarDTO);
}

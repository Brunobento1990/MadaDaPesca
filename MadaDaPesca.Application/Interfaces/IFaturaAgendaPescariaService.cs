using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IFaturaAgendaPescariaService
{
    Task<FaturaAgendaPescariaViewModel> ObterFaturaDaAgendaAsync(Guid agendaPescariaId);
    Task<FaturaAgendaPescariaViewModel> GerarFaturaDaAgendaAsync(GerarFaturaAgendaPescariaDTO gerarFaturaAgendaPescariaDTO);
    Task<FaturaAgendaPescariaViewModel> PagarFaturaDaAgendaAsync(PagarFaturaAgendaPescariaDTO pagarFaturaAgendaPescariaDTO);
}

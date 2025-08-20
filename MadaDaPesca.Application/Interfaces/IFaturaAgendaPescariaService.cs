using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Application.Interfaces;

public interface IFaturaAgendaPescariaService
{
    Task<PaginacaoModel<FaturaAgendaPescariaViewModel>> PaginacaoAsync(FilterModel<FaturaAgendaPescaria> filterModel);
    Task<FaturaAgendaPescariaViewModel> ObterFaturaDaAgendaAsync(Guid agendaPescariaId);
    Task<FaturaAgendaPescariaViewModel> ObterPorIdAsync(Guid id);
    Task EstornarAgendaCanceladaAsync(Guid agendaPescariaId);
    Task<FaturaAgendaPescariaViewModel> GerarFaturaDaAgendaAsync(GerarFaturaAgendaPescariaDTO gerarFaturaAgendaPescariaDTO);
    Task<FaturaAgendaPescariaViewModel> PagarFaturaDaAgendaAsync(PagarFaturaAgendaPescariaDTO pagarFaturaAgendaPescariaDTO);
    Task<FaturaAgendaPescariaViewModel> EstornarFaturaDaAgendaAsync(EstornarFaturaAgendaPescariaDTO estornarFaturaAgendaPescariaDTO);
}

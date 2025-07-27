using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Application.Interfaces;

public interface IEmbarcacaoService
{
    Task<PaginacaoModel<EmbarcacaoViewModel>> PaginacaoAsync(FilterModel<Embarcacao> filtro);
    Task<EmbarcacaoViewModel> CriarAsync(EmbarcacaoCreateDTO embarcacaoCreateDTO);
    Task<EmbarcacaoViewModel> EditarAsync(EmbarcacaoEditarDTO embarcacaoEditarDTO);
    Task<EmbarcacaoViewModel> ObterPorIdAsync(Guid id);
    Task ExcluirAsync(Guid id);
}

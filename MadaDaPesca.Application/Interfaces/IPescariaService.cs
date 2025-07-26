using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Application.Interfaces;

public interface IPescariaService
{
    Task<PaginacaoModel<PescariaViewModel>> PaginacaoAsync(FilterModel<Pescaria> filtro);
    Task<PescariaViewModel> CriarAsync(PescariaDTO pescariaDTO);
    Task<PescariaViewModel> EditarAsync(PescariaEditarDTO pescariaEditarDTO);
    Task<PescariaViewModel> ObterPorIdAsync(Guid id);
    Task ExcluirAsync(Guid id);
}

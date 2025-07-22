using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IHomeGuiaDePescaService
{
    Task<HomeViewModel> ObterAsync(HomeDTO homeDTO);
}

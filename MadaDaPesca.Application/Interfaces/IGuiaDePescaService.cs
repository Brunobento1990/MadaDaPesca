using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IGuiaDePescaService
{
    Task<GuiaDePescaViewModel> CreateAsync(GuiaDePescaCreateDTO guiaDePescaCreateDTO);
    Task<GuiaDePescaViewModel> MinhaContaAsync();
    Task<GuiaDePescaViewModel> ObterPerfilAsyncAsync(Guid id);
    Task<GuiaDePescaViewModel> EditarMinhaContaAsync(GuiaDePescaEditarDTO guiaDePescaEditarDTO);
}

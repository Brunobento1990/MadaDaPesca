using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface ILoginGuiaDePescaService
{
    Task<LoginGuiaDePescaViewModel> LoginAsync(LoginDTO loginDTO);
}

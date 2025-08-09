using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IHomePescadorService
{
    Task<HomePescadorViewModel> HomeAsync();
}

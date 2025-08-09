using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class HomePescadorService : IHomePescadorService
{
    private readonly IGuiaDePescaRepository _guiaDePescaRepository;

    public HomePescadorService(IGuiaDePescaRepository guiaDePescaRepository)
    {
        _guiaDePescaRepository = guiaDePescaRepository;
    }

    public async Task<HomePescadorViewModel> HomeAsync()
    {
        var home = new HomePescadorViewModel();
        var guias = await _guiaDePescaRepository.HomePescadorAsync();
        home.GuiasDePesca = guias.Select(x => (PerfilGuiaDePescaViewModel)x);

        return home;
    }
}

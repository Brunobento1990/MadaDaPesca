using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Services;

internal class HomeGuiaDePescaService : IHomeGuiaDePescaService
{
    private readonly IMarService _marService;

    public HomeGuiaDePescaService(IMarService marService)
    {
        _marService = marService;
    }

    public async Task<HomeViewModel> ObterAsync(HomeDTO homeDTO)
    {
        var homeViewModel = new HomeViewModel();

        if (homeDTO.Latitude.HasValue && homeDTO.Longitude.HasValue)
        {
            homeViewModel.InformacoesDoMar = await _marService.ObterInformacoesAsync(homeDTO.Latitude.Value, homeDTO.Longitude.Value);
        }

        return homeViewModel;
    }
}

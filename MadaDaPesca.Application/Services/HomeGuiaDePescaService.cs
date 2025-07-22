using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Services;

internal class HomeGuiaDePescaService : IHomeGuiaDePescaService
{
    private readonly IMarService _marService;
    private readonly IClimaService _climaService;

    public HomeGuiaDePescaService(IMarService marService, IClimaService climaService)
    {
        _marService = marService;
        _climaService = climaService;
    }

    public async Task<HomeViewModel> ObterAsync(HomeDTO homeDTO)
    {
        var homeViewModel = new HomeViewModel();

        if (homeDTO.Latitude.HasValue && homeDTO.Longitude.HasValue)
        {
            homeViewModel.InformacoesDoMar = await _marService.ObterInformacoesAsync(homeDTO.Latitude.Value, homeDTO.Longitude.Value);
            homeViewModel.Clima = await _climaService.ObterAsync(homeDTO.Latitude.Value, homeDTO.Longitude.Value);
        }

        return homeViewModel;
    }
}

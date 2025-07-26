using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Application.Services;

internal class HomeGuiaDePescaService : IHomeGuiaDePescaService
{
    private readonly IMarService _marService;
    private readonly IClimaService _climaService;
    private readonly IAgendaPescariaRepository _agendaPescariaRepository;
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;
    private readonly IConfiguration _configuration;

    public HomeGuiaDePescaService(IMarService marService, IClimaService climaService, IAgendaPescariaRepository agendaPescariaRepository, IGuiaDePescaLogado guiaDePescaLogado, IConfiguration configuration)
    {
        _marService = marService;
        _climaService = climaService;
        _agendaPescariaRepository = agendaPescariaRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
        _configuration = configuration;
    }

    public async Task<HomeViewModel> ObterAsync(HomeDTO homeDTO)
    {
        var homeViewModel = new HomeViewModel();

        if (homeDTO.Latitude.HasValue && homeDTO.Longitude.HasValue && _configuration["AtivarConsultaClima"]?.ToUpper() == "TRUE")
        {
            homeViewModel.InformacoesDoMar = await _marService.ObterInformacoesAsync(homeDTO.Latitude.Value, homeDTO.Longitude.Value);
            homeViewModel.Clima = await _climaService.ObterAsync(homeDTO.Latitude.Value, homeDTO.Longitude.Value);
        }

        var agendaDeHoje = await _agendaPescariaRepository.ObterAgendaDoDiaAsync(
            _guiaDePescaLogado.Id,
            dia: (short)DateTime.Now.Day,
            mes: (short)DateTime.Now.Month,
            ano: (short)DateTime.Now.Year);

        homeViewModel.AgendaDeHoje = agendaDeHoje.Select(x => (AgendaPescariaViewModel)x);

        return homeViewModel;
    }
}

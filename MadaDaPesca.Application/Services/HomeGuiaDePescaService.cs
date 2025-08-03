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
    private readonly IFaturaAgendaPescariaRepository _faturaAgendaPescariaRepository;

    public HomeGuiaDePescaService(IMarService marService, IClimaService climaService, IAgendaPescariaRepository agendaPescariaRepository, IGuiaDePescaLogado guiaDePescaLogado, IConfiguration configuration, IFaturaAgendaPescariaRepository faturaAgendaPescariaRepository)
    {
        _marService = marService;
        _climaService = climaService;
        _agendaPescariaRepository = agendaPescariaRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
        _configuration = configuration;
        _faturaAgendaPescariaRepository = faturaAgendaPescariaRepository;
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

        var agendaDeAmanha = await _agendaPescariaRepository.ObterAgendaDoDiaAsync(
            _guiaDePescaLogado.Id,
            dia: (short)(DateTime.Now.Day + 1),
            mes: (short)DateTime.Now.Month,
            ano: (short)DateTime.Now.Year);

        homeViewModel.AgendaDeHoje = agendaDeHoje.Select(x => (AgendaPescariaViewModel)x);
        homeViewModel.AgendaDeAmanha = agendaDeAmanha.Select(x => (AgendaPescariaViewModel)x);

        var faturas = await _faturaAgendaPescariaRepository.FaturasHomeGuiaDePescaAsync(_guiaDePescaLogado.Id);

        if (faturas.Count > 0)
        {
            homeViewModel.Fatura = new FaturaHomeViewModel
            {
                Ano = faturas.First().DataDeVencimento.Year,
                Mes = faturas.First().DataDeVencimento.Month,
                ValorTotal = faturas.Sum(x => x.Valor),
                ValorAReceber = faturas.Sum(x => x.ValorAReceber),
                ValorRecebido = faturas.Sum(x => x.ValorRecebido),
            };
        }

        homeViewModel.Transacoes = await _faturaAgendaPescariaRepository.TranasoesParaHomeAsync(_guiaDePescaLogado.Id);

        return homeViewModel;
    }
}

using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Infra.Enum;
using MadaDaPesca.Infra.HttpClient.DTOs;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Infra.HttpClient.Client;

internal class MarHttpClient : IMarHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public MarHttpClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<InformacoesDoMarViewModel> ObterInformacoesAsync(double latitude, double longitude)
    {
        var previsaoDeDias = int.Parse(_configuration["Api:OpenMeteo:PrevisaoDeDias"]!);
        var client = _httpClientFactory.CreateClient($"{HttpClientEnum.Open_meteo}");
        var url = $"/v1/marine?latitude={latitude.ToString().Replace(",", ".")}&longitude={longitude.ToString().Replace(",", ".")}&hourly=wave_height,sea_surface_temperature,sea_level_height_msl&forecast_days={previsaoDeDias}";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            LogService.LogApi(erro);
            throw new ValidacaoException($"Erro ao obter dados de altura de onda");
        }

        var body = await response.Content.ReadAsStreamAsync();
        var resultado = JsonSerializerAdapters.FromJson<AlturaOndaDTO>(body)
            ?? throw new ValidacaoException("Não foi possível converter os dados das alturas das ondas");

        return new InformacoesDoMarViewModel
        {
            Dias = resultado.Hourly.Time,
            AlturasDasOndas = resultado.Hourly.Wave_height,
            PrevisaoDeDias = previsaoDeDias,
            AlturasDaMare = resultado.Hourly.Sea_level_height_msl,
            TemperaturasDoMar = resultado.Hourly.Sea_surface_temperature,
            UnidadeDeMedida = new UnidadeDeMedidaMarViewModel
            {
                AlturaDaMare = resultado.Hourly_units.Wave_height,
                AlturaDaOnda = resultado.Hourly_units.Sea_level_height_msl,
                TemperaturaDoMar = resultado.Hourly_units.Sea_surface_temperature
            }
        };
    }
}

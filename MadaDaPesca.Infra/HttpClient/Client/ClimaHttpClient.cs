using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Infra.Enum;
using MadaDaPesca.Infra.HttpClient.DTOs;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Infra.HttpClient.Client;

internal class ClimaHttpClient : IClimaHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ClimaHttpClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<ClimaViewModel> ObterAsync(double latitude, double longitude)
    {
        var previsaoDeDias = int.Parse(_configuration["Api:OpenMeteo:PrevisaoDeDias"]!);
        var client = _httpClientFactory.CreateClient($"{HttpClientEnum.Open_meteo_Clima}");
        var url = $"/v1/forecast?latitude={latitude.ToString().Replace(",", ".")}&longitude={longitude.ToString().Replace(",", ".")}&hourly=temperature_2m,rain,wind_speed_180m,wind_direction_180m,temperature_180m,surface_pressure&forecast_days={previsaoDeDias}";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            LogService.LogApi(erro);
            throw new ValidacaoException($"Erro ao obter dados do clima");
        }

        var body = await response.Content.ReadAsStreamAsync();
        var resultado = JsonSerializerAdapters.FromJson<ClimaDTO>(body)
            ?? throw new ValidacaoException("Não foi possível converter os dados da api de clima");

        return new ClimaViewModel
        {
            Chuva = resultado.Hourly.Rain,
            Dias = resultado.Hourly.Time,
            DirecaoDoVento = resultado.Hourly.Wind_direction_180m,
            PressaoAtmosferica = resultado.Hourly.Surface_pressure,
            PrevisaoDeDias = previsaoDeDias,
            Temperaturas = resultado.Hourly.Temperature_180m,
            UnidadeDeMedida = new UnidadeDeMedidaClimaViewModel
            {
                Chuva = resultado.Hourly_units.Rain,
                DirecaoDoVento = resultado.Hourly_units.Wind_direction_180m,
                PressaoAtmosferica = resultado.Hourly_units.Surface_pressure,
                Temperatura = resultado.Hourly_units.Temperature_180m,
                VelocidadeDoVento = resultado.Hourly_units.Wind_speed_180m
            },
            VelocidadeDoVento = resultado.Hourly.Wind_speed_180m
        };
    }
}

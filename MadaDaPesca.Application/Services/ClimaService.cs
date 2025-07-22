using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Services;

internal class ClimaService : IClimaService
{
    private readonly IClimaHttpClient _climaHttpClient;

    public ClimaService(IClimaHttpClient climaHttpClient)
    {
        _climaHttpClient = climaHttpClient;
    }

    public Task<ClimaViewModel> ObterAsync(double latitude, double longitude)
        => _climaHttpClient.ObterAsync(latitude, longitude);
}

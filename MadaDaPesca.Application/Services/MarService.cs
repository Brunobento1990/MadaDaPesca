using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Services;

internal class MarService : IMarService
{
    private readonly IMarHttpClient _marHttpClient;

    public MarService(IMarHttpClient marHttpClient)
    {
        _marHttpClient = marHttpClient;
    }

    public Task<InformacoesDoMarViewModel> ObterInformacoesAsync(double latitude, double longitude)
        => _marHttpClient.ObterInformacoesAsync(latitude, longitude);
}

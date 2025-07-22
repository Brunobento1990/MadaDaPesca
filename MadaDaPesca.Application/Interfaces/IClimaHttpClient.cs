using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IClimaHttpClient
{
    Task<ClimaViewModel> ObterAsync(double latitude, double longitude);
}

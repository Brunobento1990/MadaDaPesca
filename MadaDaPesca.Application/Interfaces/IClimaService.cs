using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IClimaService
{
    Task<ClimaViewModel> ObterAsync(double latitude, double longitude);
}

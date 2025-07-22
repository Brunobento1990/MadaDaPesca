using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IMarHttpClient
{
    Task<InformacoesDoMarViewModel> ObterInformacoesAsync(double latitude, double longitude);
}

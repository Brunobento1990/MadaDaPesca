using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IMarService
{
    Task<InformacoesDoMarViewModel> ObterInformacoesAsync(double latitude, double longitude);
}

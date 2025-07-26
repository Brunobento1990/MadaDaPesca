using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IGoogleAuthHttpClient
{
    Task<TokenResponseGoogleViewModel> ValidarTokenGoogleAsync(string token);
}

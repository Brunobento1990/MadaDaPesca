using Google.Apis.Auth;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Infra.HttpClient.Client;

internal class GoogleAuthHttpClient : IGoogleAuthHttpClient
{
    private readonly IConfiguration _configuration;

    public GoogleAuthHttpClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TokenResponseGoogleViewModel> ValidarTokenGoogleAsync(string token)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _configuration["Api:Google:KeyLogin"] }
        });

        return new TokenResponseGoogleViewModel()
        {
            Email = payload.Email,
            Foto = payload.Picture,
            Nome = $"{payload.GivenName} {payload.FamilyName}"
        };
    }
}

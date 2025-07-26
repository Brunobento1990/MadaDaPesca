using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Application.Services;

internal class LoginGuiaDePescaService : ILoginGuiaDePescaService
{
    private readonly ILoginGuiaDePescaRepository _loginGuiaDePescaRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IGoogleAuthHttpClient _googleAuthHttpClient;
    public LoginGuiaDePescaService(ILoginGuiaDePescaRepository loginGuiaDePescaRepository, ITokenService tokenService, IConfiguration configuration, IGoogleAuthHttpClient googleAuthHttpClient)
    {
        _loginGuiaDePescaRepository = loginGuiaDePescaRepository;
        _tokenService = tokenService;
        _configuration = configuration;
        _googleAuthHttpClient = googleAuthHttpClient;
    }

    public async Task<LoginGuiaDePescaViewModel> LoginAsync(LoginDTO loginDTO)
    {
        var guia = await _loginGuiaDePescaRepository.LoginAsync(loginDTO.Cpf.LimparMascaraCpf())
            ?? throw new ValidacaoException("Não foi possível localizar seu cadastro");

        guia.ValidarAcesso();

        if (!PasswordAdapter.VerifyPassword(loginDTO.Senha, guia.AcessoGuiaDePesca.Senha))
        {
            throw new ValidacaoException("Senha incorreta");
        }

        var (token, refreshToken) = _tokenService.GerarTokenGuiaDePesca(guia);

        return new()
        {
            GuiaDePesca = (GuiaDePescaViewModel)guia,
            TokenSchema = _configuration["Jwt:Schema"]!,
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginGuiaDePescaViewModel> LoginComGoogleAsync(LoginComGoogleDTO loginComGoogleDTO)
    {
        var responseGoogle = await _googleAuthHttpClient.ValidarTokenGoogleAsync(loginComGoogleDTO.Token);
        var guia = await _loginGuiaDePescaRepository.LoginComGoogleAsync(responseGoogle.Email)
            ?? throw new ValidacaoException("Não foi possível localizar seu cadastro");

        guia.ValidarAcesso();

        var (token, refreshToken) = _tokenService.GerarTokenGuiaDePesca(guia);

        return new()
        {
            GuiaDePesca = (GuiaDePescaViewModel)guia,
            TokenSchema = _configuration["Jwt:Schema"]!,
            Token = token,
            RefreshToken = refreshToken
        };
    }
}

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
    public LoginGuiaDePescaService(ILoginGuiaDePescaRepository loginGuiaDePescaRepository, ITokenService tokenService, IConfiguration configuration)
    {
        _loginGuiaDePescaRepository = loginGuiaDePescaRepository;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<LoginGuiaDePescaViewModel> LoginAsync(LoginDTO loginDTO)
    {
        var guia = await _loginGuiaDePescaRepository.LoginAsync(loginDTO.Cpf.LimparMascaraCpf())
            ?? throw new ValidacaoException("Não foi possível localizar seu cadastro");

        if (guia.AcessoGuiaDePesca.AcessoBloqueado)
        {
            throw new ValidacaoException("Seu acesso está bloqueado, entre em contato com o suporte");
        }

        if (!guia.AcessoGuiaDePesca.EmailVerificado)
        {
            throw new ValidacaoException("Seu e-mail não foi verificado, verifique sua caixa de entrada ou spam");
        }

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
}

using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class RecuperarSenhaGuiaDePescaService : IRecuperarSenhaGuiaDePescaService
{
    private readonly IGuiaDePescaRepository _guiaDePescaRepository;

    public RecuperarSenhaGuiaDePescaService(IGuiaDePescaRepository guiaDePescaRepository)
    {
        _guiaDePescaRepository = guiaDePescaRepository;
    }

    public async Task RecuperarSenhaAsync(RecuperarSenhaDTO recuperarSenhaDTO)
    {
        recuperarSenhaDTO.Validar();

        var guia = await _guiaDePescaRepository.ObterPorTokenEsqueceuSenhaAsync(recuperarSenhaDTO.TokenEsqueceuSenha)
            ?? throw new ValidacaoException("Não foi possível localizar seu cadastro para recuperar a senha");

        if (!guia.AcessoGuiaDePesca.ExpiracaoTokenEsqueceuSenha.HasValue)
        {
            throw new ValidacaoException("Token inválido ou expirado");
        }

        if ((guia.AcessoGuiaDePesca.ExpiracaoTokenEsqueceuSenha.Value - DateTime.Now).Minutes > 60)
        {
            throw new ValidacaoException("Token expirado, recupere a senha novamente");
        }

        guia.AcessoGuiaDePesca.RecuperarSenha(PasswordAdapter.GenerateHash(recuperarSenhaDTO.Senha));

        _guiaDePescaRepository.Editar(guia);
        await _guiaDePescaRepository.SaveChangesAsync();
    }
}

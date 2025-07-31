using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class ConfirmarContaGuiaDePescaService : IConfirmarContaGuiaDePescaService
{
    private readonly IGuiaDePescaRepository _guiaDePescaRepository;

    public ConfirmarContaGuiaDePescaService(IGuiaDePescaRepository guiaDePescaRepository)
    {
        _guiaDePescaRepository = guiaDePescaRepository;
    }

    public async Task ConfirmarContaAsync(Guid token)
    {
        var guia = await _guiaDePescaRepository.ObterPorIdAsync(token)
            ?? throw new ValidacaoException("Não foi possível localizar seu cadastro");

        guia.AcessoGuiaDePesca.VerificarEmail(true);

        _guiaDePescaRepository.Editar(guia);
        await _guiaDePescaRepository.SaveChangesAsync();
    }
}

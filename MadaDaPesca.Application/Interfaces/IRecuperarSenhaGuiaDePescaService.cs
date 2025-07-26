using MadaDaPesca.Application.DTOs;

namespace MadaDaPesca.Application.Interfaces;

public interface IRecuperarSenhaGuiaDePescaService
{
    Task RecuperarSenhaAsync(RecuperarSenhaDTO recuperarSenhaDTO);
}

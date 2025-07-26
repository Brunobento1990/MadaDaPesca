using MadaDaPesca.Application.DTOs;

namespace MadaDaPesca.Application.Interfaces;

public interface IEsqueceuSenhaGuiaDePescaService
{
    Task EsqueceuSenhaAsync(EsqueceSenhaDTO esqueceSenhaDTO);
}

using MadaDaPesca.Application.DTOs;

namespace MadaDaPesca.Application.Interfaces;

public interface IEmailService
{
    Task<bool> EnviarAsync(EnvioEmailDTO envioEmailDTO);
}

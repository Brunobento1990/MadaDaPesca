using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.Interfaces;

public interface ITokenService
{
    (string token, string refreshToken) GerarTokenGuiaDePesca(GuiaDePesca guiaDePesca);
}

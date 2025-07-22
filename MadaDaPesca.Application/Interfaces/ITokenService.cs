using MadaDaPesca.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace MadaDaPesca.Application.Interfaces;

public interface ITokenService
{
    string Issue { get; set; }
    string Audience { get; set; }
    SigningCredentials Credenciais { get; set; }
    SymmetricSecurityKey Key { get; set; }
    (string token, string refreshToken) GerarTokenGuiaDePesca(GuiaDePesca guiaDePesca);
}

using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface ILoginGuiaDePescaRepository
{
    Task<GuiaDePesca?> LoginAsync(string cpf);
    Task<GuiaDePesca?> LoginComGoogleAsync(string email);
}

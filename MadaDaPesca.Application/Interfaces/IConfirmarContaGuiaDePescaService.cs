namespace MadaDaPesca.Application.Interfaces;

public interface IConfirmarContaGuiaDePescaService
{
    Task ConfirmarContaAsync(Guid token);
}

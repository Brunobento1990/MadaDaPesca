using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IGuiaDePescaRepository : IGenericRepository<GuiaDePesca>
{
    Task<GuiaDePesca?> ObterParaValidarAsync(string cpf, string email);
    Task<GuiaDePesca?> ObterParaValidarAcessoAsync(Guid id);
}

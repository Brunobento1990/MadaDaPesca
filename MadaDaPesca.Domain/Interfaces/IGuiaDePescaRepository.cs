using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IGuiaDePescaRepository : IGenericRepository<GuiaDePesca>
{
    Task<GuiaDePesca?> ObterParaValidarAsync(string cpf, string email, Guid? idDiferente = null);
    Task<GuiaDePesca?> ObterParaValidarAcessoAsync(Guid id);
    Task<GuiaDePesca?> ObterPorIdAsync(Guid id);
    Task<GuiaDePesca?> ObterPorCpfAsync(string cpf);
    Task<GuiaDePesca?> ObterPorTokenEsqueceuSenhaAsync(Guid tokenEsqueceuSenha);
}

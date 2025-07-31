using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IPescariaRepository : IGenericRepository<Pescaria>
{
    Task<Pescaria?> ObterPorIdAsync(Guid id);
    Task<IList<Pescaria>> ObterPescariasDoGuiaAsync(Guid guiaDePescaId);
    Task<IList<BloqueioDataPescaria>> ObterDatasBloqueadasAsync(Guid pescariaId);
    Task<IList<BloqueioDataPescaria>> ObterTodasDatasBloqueadasAsync(int mes, int ano, Guid guiaDePescaId);
    void RemoverDatasBloqueadas(IList<BloqueioDataPescaria> datas);
    Task AddDatasBloqueadasAsync(IList<BloqueioDataPescaria> datas);
}

using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IPescariaRepository : IGenericRepository<Pescaria>
{
    Task<Pescaria?> ObterPorIdAsync(Guid id);
    Task<IList<Pescaria>> ObterPescariasDoGuiaAsync(Guid guiaDePescaId);
}

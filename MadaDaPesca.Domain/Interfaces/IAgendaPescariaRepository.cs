using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IAgendaPescariaRepository : IGenericRepository<AgendaPescaria>
{
    Task<IList<AgendaPescaria>> ObterAgendaDaPescariaDoDiaAsync(Guid pescariaId, short dia, short mes, short ano);
    Task<IList<AgendaPescaria>> ObterAgendaDoDiaAsync(Guid guiaDePescaId, short dia, short mes, short ano);
    Task<IList<AgendaPescaria>> ObterAgendaDaPescariaDoMesAsync(Guid guiaDePescaId, short mes, short ano);
    Task<AgendaPescaria?> ObterPorIdAsync(Guid id);
    Task AddGaleriaAsync(IList<GaleriaAgendaPescaria> galeriaAgendaPescarias);
}

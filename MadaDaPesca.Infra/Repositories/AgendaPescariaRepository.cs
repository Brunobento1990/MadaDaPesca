using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class AgendaPescariaRepository : GenericRepository<AgendaPescaria>, IAgendaPescariaRepository
{
    public AgendaPescariaRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task AddGaleriaAsync(IList<GaleriaAgendaPescaria> galeriaAgendaPescarias)
    {
        await AppDbContext
            .GaleriaAgendaPescaria
            .AddRangeAsync(galeriaAgendaPescarias);
    }

    public async Task<IList<AgendaPescaria>> ObterAgendaDaPescariaDoDiaAsync(Guid pescariaId, short dia, short mes, short ano)
    {
        return await AppDbContext
            .AgendaPescarias
            .Where(a => a.PescariaId == pescariaId && !a.Excluido && a.Dia == dia && a.Mes == mes && a.Ano == ano)
            .ToListAsync();
    }

    public async Task<IList<AgendaPescaria>> ObterAgendaDaPescariaDoMesAsync(Guid guiaDePescaId, short mes, short ano)
    {
        return await AppDbContext
            .AgendaPescarias
            .Include(a => a.Pescaria)
            .Where(a => a.Pescaria.GuiaDePescaId == guiaDePescaId && !a.Excluido && a.Mes == mes && a.Ano == ano)
            .ToListAsync();
    }

    public async Task<IList<AgendaPescaria>> ObterAgendaDoDiaAsync(Guid guiaDePescaId, short dia, short mes, short ano)
    {
        return await AppDbContext
            .AgendaPescarias
            .AsNoTracking()
            .Include(x => x.Pescaria)
            .Where(x => x.Pescaria.GuiaDePescaId == guiaDePescaId && x.Dia == dia && x.Mes == mes && x.Ano == ano)
            .ToListAsync();
    }

    public async Task<AgendaPescaria?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .AgendaPescarias
            .Include(x => x.Pescaria)
            .Include(x => x.Galeria)
            .FirstOrDefaultAsync(a => a.Id == id && !a.Excluido);
    }
}

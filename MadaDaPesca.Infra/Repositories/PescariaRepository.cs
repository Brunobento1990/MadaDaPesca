using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class PescariaRepository : GenericRepository<Pescaria>, IPescariaRepository
{
    public PescariaRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task AddDatasBloqueadasAsync(IList<BloqueioDataPescaria> datas)
    {
        await AppDbContext.DatasBloqueadas.AddRangeAsync(datas);
    }

    public async Task<IList<BloqueioDataPescaria>> ObterDatasBloqueadasAsync(Guid pescariaId)
    {
        return await AppDbContext
            .DatasBloqueadas
            .Where(x => x.PescariaId == pescariaId)
            .ToListAsync();
    }

    public async Task<IList<Pescaria>> ObterPescariasDoGuiaAsync(Guid guiaDePescaId)
    {
        return await AppDbContext
            .Pescarias
            .AsNoTracking()
            .Where(x => x.GuiaDePescaId == guiaDePescaId && !x.Excluido)
            .ToListAsync();
    }

    public async Task<Pescaria?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .Pescarias
            .Include(x => x.Embarcacao)
            .Include(x => x.DatasBloqueadas)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<BloqueioDataPescaria>> ObterTodasDatasBloqueadasAsync(int mes, int ano, Guid guiaDePescaId)
    {
        return await AppDbContext
            .DatasBloqueadas
            .Where(x => x.Pescaria.GuiaDePescaId == guiaDePescaId &&
                        x.Data.Month == mes &&
                        x.Data.Year == ano)
            .ToListAsync();
    }

    public void RemoverDatasBloqueadas(IList<BloqueioDataPescaria> datas)
    {
        AppDbContext.DatasBloqueadas.RemoveRange(datas);
    }
}

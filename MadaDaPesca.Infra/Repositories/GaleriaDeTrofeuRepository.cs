using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class GaleriaDeTrofeuRepository : GenericRepository<GaleriaDeTrofeu>, IGaleriaDeTrofeuRepository
{
    public GaleriaDeTrofeuRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public void Excluir(GaleriaDeTrofeu galeriaDeTrofeu)
    {
        AppDbContext.GaleriaDeTrofeus.Remove(galeriaDeTrofeu);
    }

    public async Task<IList<GaleriaDeTrofeu>> GaleriaDoGuiaDePescaAsync(Guid guiaDePescaId)
    {
        return await AppDbContext
            .GaleriaDeTrofeus
            .AsNoTracking()
            .Where(x => !x.Excluido && x.GuiaDePescaId == guiaDePescaId)
            .ToListAsync();
    }

    public async Task<GaleriaDeTrofeu?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .GaleriaDeTrofeus
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}

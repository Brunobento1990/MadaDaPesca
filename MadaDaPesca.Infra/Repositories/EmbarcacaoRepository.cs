using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class EmbarcacaoRepository : GenericRepository<Embarcacao>, IEmbarcacaoRepository
{
    public EmbarcacaoRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task AddGaleriaAsync(IList<GaleriaFotoEmbarcacao> galeria)
    {
        await AppDbContext.GaleriaFotoEmbarcacoes.AddRangeAsync(galeria);
    }

    public async Task<Embarcacao?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .Embarcacoes
            .Include(x => x.Galeria)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void RemoverGaleria(IList<GaleriaFotoEmbarcacao> galeria)
    {
        AppDbContext.GaleriaFotoEmbarcacoes.RemoveRange(galeria);
    }
}

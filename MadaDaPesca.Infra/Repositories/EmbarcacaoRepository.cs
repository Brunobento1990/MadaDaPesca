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

    public async Task<Embarcacao?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .Embarcacoes
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}

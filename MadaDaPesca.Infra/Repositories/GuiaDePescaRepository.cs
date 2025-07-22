using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class GuiaDePescaRepository : GenericRepository<GuiaDePesca>, IGuiaDePescaRepository
{
    public GuiaDePescaRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<GuiaDePesca?> ObterParaValidarAcessoAsync(Guid id)
    {
        return await AppDbContext
            .GuiasDePesca
            .AsNoTracking()
            .Include(g => g.AcessoGuiaDePesca)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<GuiaDePesca?> ObterParaValidarAsync(string cpf, string email)
    {
        return await AppDbContext
            .GuiasDePesca
            .AsNoTracking()
            .Include(g => g.Pessoa)
            .FirstOrDefaultAsync(x => x.Pessoa.Cpf == cpf || x.Pessoa.Email == email);
    }
}

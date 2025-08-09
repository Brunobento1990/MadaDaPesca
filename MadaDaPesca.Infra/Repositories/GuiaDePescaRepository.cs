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

    public async Task<GuiaDePesca?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .GuiasDePesca
            .Include(g => g.Pessoa)
            .Include(g => g.AcessoGuiaDePesca)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<GuiaDePesca?> ObterParaValidarAcessoAsync(Guid id)
    {
        return await AppDbContext
            .GuiasDePesca
            .AsNoTracking()
            .Include(g => g.AcessoGuiaDePesca)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<GuiaDePesca?> ObterParaValidarAsync(string cpf, string email, Guid? idDiferente = null)
    {
        var query = AppDbContext.GuiasDePesca
            .AsNoTracking()
            .Include(g => g.Pessoa)
            .Where(x => x.Pessoa.Cpf == cpf || x.Pessoa.Email == email);

        if (idDiferente.HasValue)
        {
            query = query.Where(x => x.Id != idDiferente.Value);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<GuiaDePesca?> ObterPorCpfAsync(string cpf)
    {
        return await AppDbContext
            .GuiasDePesca
            .Include(g => g.Pessoa)
            .Include(g => g.AcessoGuiaDePesca)
            .FirstOrDefaultAsync(x => x.Pessoa.Cpf == cpf && !x.Excluido);
    }

    public async Task<GuiaDePesca?> ObterPorTokenEsqueceuSenhaAsync(Guid tokenEsqueceuSenha)
    {
        return await AppDbContext
            .GuiasDePesca
            .Include(g => g.Pessoa)
            .Include(g => g.AcessoGuiaDePesca)

            .FirstOrDefaultAsync(x => x.AcessoGuiaDePesca.TokenEsqueceuSenha == tokenEsqueceuSenha);
    }

    public async Task<IList<GuiaDePesca>> HomePescadorAsync()
    {
        return await AppDbContext
            .GuiasDePesca
            .AsNoTracking()
            .Include(x => x.Pessoa)
            .Skip(0)
            .Take(3)
            .OrderByDescending(x => x.DataDeCadastro)
            .Where(x => !x.Excluido)
            .ToListAsync();
    }

    public async Task<GuiaDePesca?> ObterPoraPerfilAsync(Guid id)
    {
        return await AppDbContext
            .GuiasDePesca
            .AsNoTracking()
            .Include(g => g.Pessoa)
            .Include(g => g.Pescarias!)
                .ThenInclude(x => x.Embarcacao)
            .Include(g => g.GaleriaDeTrofeu)
            .FirstOrDefaultAsync(x => x.Id == id && !x.Excluido);
    }
}

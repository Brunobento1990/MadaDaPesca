using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Repositories;

internal class FaturaAgendaPescariaRepository : GenericRepository<FaturaAgendaPescaria>, IFaturaAgendaPescariaRepository
{
    public FaturaAgendaPescariaRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task AddTransacaoAsync(TransacaoFaturaAgendaPescaria transacaoFaturaAgendaPescaria)
    {
        await AppDbContext.TransacoesFaturaAgenda.AddAsync(transacaoFaturaAgendaPescaria);
    }

    public async Task<IList<FaturaAgendaPescaria>> FaturasHomeGuiaDePescaAsync(Guid guiaDePescaId)
    {
        var data = DateTime.Now;
        return await AppDbContext
            .FaturasAgendaPescarias
            .AsNoTracking()
            .Include(x => x.Transacoes)
            .Where(x =>
                x.GuiaDePescaId == guiaDePescaId &&
                !x.Excluido &&
                x.DataDeVencimento.Month == data.Month &&
                x.DataDeVencimento.Year == data.Year)
            .ToListAsync();
    }

    public async Task<FaturaAgendaPescaria?> ObterFaturaDoAgendamentoAsync(Guid agendaPescariaId, Guid guiaDePescaId)
    {
        return await AppDbContext
            .FaturasAgendaPescarias
            .Include(x => x.Transacoes)
            .FirstOrDefaultAsync(x => !x.Excluido &&
                                      x.AgendaPescariaId == agendaPescariaId &&
                                      x.GuiaDePescaId == guiaDePescaId);
    }

    public async Task<FaturaAgendaPescaria?> ObterPorIdAsync(Guid id, Guid guiaDePescaId)
    {
        return await AppDbContext
            .FaturasAgendaPescarias
            .Include(x => x.Transacoes)
            .Include(x => x.AgendaPescaria)
            .FirstOrDefaultAsync(x => !x.Excluido && x.Id == id && x.GuiaDePescaId == guiaDePescaId);
    }
}

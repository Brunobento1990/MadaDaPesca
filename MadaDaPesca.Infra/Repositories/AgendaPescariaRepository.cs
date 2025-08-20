using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Domain.Models;
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

    public void ExcluirGaleria(IList<GaleriaAgendaPescaria> galeriaAgendaPescarias)
    {
        AppDbContext
            .GaleriaAgendaPescaria
            .RemoveRange(galeriaAgendaPescarias);
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
            .Where(x => x.Pescaria.GuiaDePescaId == guiaDePescaId && x.Dia == dia && x.Mes == mes && x.Ano == ano && !x.Excluido)
            .ToListAsync();
    }

    public Task<IList<AgendaPescaria>> ObterAgendaParaNotificarAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AgendaPescaria?> ObterPorIdAsync(Guid id)
    {
        return await AppDbContext
            .AgendaPescarias
            .Include(x => x.Pescaria)
                .ThenInclude(x => x.DatasBloqueadas)
            .Include(x => x.Galeria)
            .FirstOrDefaultAsync(a => a.Id == id && !a.Excluido);
    }

    public async Task<VariacaoMensalAgendamentoHomeModel> VariacaoMensalAsync(Guid guiaDePescaId)
    {
        var hoje = DateTime.Today;
        var mes = hoje.Month;
        var anoAtual = hoje.Year;
        var anoAnterior = anoAtual - 1;

        var totais = await AppDbContext
            .AgendaPescarias
            .AsNoTracking()
            .Where(i =>
                !i.Excluido &&
                i.Mes == mes &&
                (i.Ano == anoAtual || i.Ano == anoAnterior)
            )
            .GroupBy(i => i.Ano)
            .Select(g => new
            {
                Ano = g.Key,
                Total = g.Count()
            })
            .ToListAsync();

        var totalAnoAtual = totais.FirstOrDefault(x => x.Ano == anoAtual)?.Total ?? 0;
        var totalAnoAnterior = totais.FirstOrDefault(x => x.Ano == anoAnterior)?.Total ?? 0;

        decimal variacao = 0;

        if (totalAnoAnterior == 0)
        {
            variacao = totalAnoAtual == 0 ? 0 : 100;
        }
        else
        {
            variacao = (decimal)(totalAnoAtual - totalAnoAnterior) / totalAnoAnterior * 100;
        }

        return new VariacaoMensalAgendamentoHomeModel()
        {
            Mes = mes,
            TotalAnoAnterior = totalAnoAnterior,
            TotalAnoAtual = totalAnoAtual,
            Porcentagem = variacao,
            AnoAnterior = anoAnterior,
            AnoAtual = anoAtual
        };
    }
}

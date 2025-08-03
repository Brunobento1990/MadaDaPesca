using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Domain.Interfaces;

public interface IFaturaAgendaPescariaRepository : IGenericRepository<FaturaAgendaPescaria>
{
    Task<IList<FaturaAgendaPescaria>> FaturasHomeGuiaDePescaAsync(Guid guiaDePescaId);
    Task<FaturaAgendaPescaria?> ObterFaturaDoAgendamentoAsync(Guid agendaPescariaId, Guid guiaDePescaId);
    Task<FaturaAgendaPescaria?> ObterPorIdAsync(Guid id, Guid guiaDePescaId);
    Task AddTransacaoAsync(TransacaoFaturaAgendaPescaria transacaoFaturaAgendaPescaria);
    Task<IList<FaturaHomeModel>> TranasoesParaHomeAsync(Guid guiaDePescaId);
}

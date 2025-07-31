using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IEmbarcacaoRepository : IGenericRepository<Embarcacao>
{
    Task<Embarcacao?> ObterPorIdAsync(Guid id);
    void RemoverGaleria(IList<GaleriaFotoEmbarcacao> galeria);
    Task AddGaleriaAsync(IList<GaleriaFotoEmbarcacao> galeria);
}

using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Domain.Interfaces;

public interface IGaleriaDeTrofeuRepository : IGenericRepository<GaleriaDeTrofeu>
{
    Task<IList<GaleriaDeTrofeu>> GaleriaDoGuiaDePescaAsync(Guid guiaDePescaId);
    Task<GaleriaDeTrofeu?> ObterPorIdAsync(Guid id);
    void Excluir(GaleriaDeTrofeu galeriaDeTrofeu);
}

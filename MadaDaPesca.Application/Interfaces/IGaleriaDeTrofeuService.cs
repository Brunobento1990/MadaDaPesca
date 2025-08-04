using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.ViewModel;

namespace MadaDaPesca.Application.Interfaces;

public interface IGaleriaDeTrofeuService
{
    Task<IEnumerable<GaleriaDeTrofeuViewModel>> GaleriaDoGuiaDePescaAsync();
    Task<IEnumerable<GaleriaDeTrofeuViewModel>> AdicionarAsync(IList<GaleriaDeTrofeuDTO> galeria);
    Task ExcluirAsync(Guid id);
}

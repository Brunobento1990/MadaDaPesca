using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class GaleriaDeTrofeuService : IGaleriaDeTrofeuService
{
    private readonly IGaleriaDeTrofeuRepository _galeriaDeTrofeuRepository;
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;
    private readonly IUploadImagemService _uploadImageService;

    public GaleriaDeTrofeuService(IGaleriaDeTrofeuRepository galeriaDeTrofeuRepository, IGuiaDePescaLogado guiaDePescaLogado, IUploadImagemService uploadImageService)
    {
        _galeriaDeTrofeuRepository = galeriaDeTrofeuRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
        _uploadImageService = uploadImageService;
    }

    public async Task<IEnumerable<GaleriaDeTrofeuViewModel>> AdicionarAsync(IList<GaleriaDeTrofeuDTO> galeria)
    {
        IList<GaleriaDeTrofeuViewModel> galeriaView = [];
        foreach (var item in galeria)
        {
            var id = Guid.NewGuid();
            var url = await _uploadImageService.UploadAsync(item.Base64, id);

            var foto = GaleriaDeTrofeu.Novo(_guiaDePescaLogado.Id, url, item.Descricao, id);

            await _galeriaDeTrofeuRepository.AddAsync(foto);

            galeriaView.Add((GaleriaDeTrofeuViewModel)foto);
        }

        await _galeriaDeTrofeuRepository.SaveChangesAsync();

        return galeriaView;
    }

    public async Task ExcluirAsync(Guid id)
    {
        var foto = await _galeriaDeTrofeuRepository.ObterPorIdAsync(id)
            ?? throw new ValidacaoException("Não foi possível localizar a foto selecionada");

        await _uploadImageService.DeleteAsync(foto.Id);
        _galeriaDeTrofeuRepository.Excluir(foto);
        await _galeriaDeTrofeuRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<GaleriaDeTrofeuViewModel>> GaleriaDoGuiaDePescaAsync()
    {
        var galeria = await _galeriaDeTrofeuRepository.GaleriaDoGuiaDePescaAsync(_guiaDePescaLogado.Id);
        return galeria.Select(g => (GaleriaDeTrofeuViewModel)g);
    }
}

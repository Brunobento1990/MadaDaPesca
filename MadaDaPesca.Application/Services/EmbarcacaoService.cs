using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Application.Services;

internal class EmbarcacaoService : IEmbarcacaoService
{
    private readonly IEmbarcacaoRepository _embarcacaoRepository;
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;
    private readonly IUploadImagemService _uploadImagemService;

    public EmbarcacaoService(IEmbarcacaoRepository embarcacaoRepository, IGuiaDePescaLogado guiaDePescaLogado, IUploadImagemService uploadImagemService)
    {
        _embarcacaoRepository = embarcacaoRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
        _uploadImagemService = uploadImagemService;
    }

    public async Task<EmbarcacaoViewModel> CriarAsync(EmbarcacaoCreateDTO embarcacaoCreateDTO)
    {
        var embarcacao = Embarcacao.Criar(
            nome: embarcacaoCreateDTO.Nome,
            motor: embarcacaoCreateDTO.Motor,
            motorEletrico: embarcacaoCreateDTO.MotorEletrico,
            largura: embarcacaoCreateDTO.Largura,
            comprimento: embarcacaoCreateDTO.Comprimento,
            quantidadeDeLugar: embarcacaoCreateDTO.QuantidadeDeLugar,
            guiaDePescaId: _guiaDePescaLogado.Id);

        embarcacao.Galeria ??= [];

        foreach (var foto in embarcacaoCreateDTO.GaleriaValida)
        {
            var id = Guid.NewGuid();
            var url = foto.Url;

            if (!url.StartsWith("http"))
            {
                url = await _uploadImagemService.UploadAsync(foto.Url, id);
            }

            embarcacao.Galeria.Add(new GaleriaFotoEmbarcacao(id, url, embarcacao.Id));
        }

        await _embarcacaoRepository.AddAsync(embarcacao);
        await _embarcacaoRepository.SaveChangesAsync();

        return (EmbarcacaoViewModel)embarcacao;
    }

    public async Task<EmbarcacaoViewModel> EditarAsync(EmbarcacaoEditarDTO embarcacaoEditarDTO)
    {
        var embarcacao = await ObterAsync(embarcacaoEditarDTO.Id);

        embarcacao.Editar(
            nome: embarcacaoEditarDTO.Nome,
            motor: embarcacaoEditarDTO.Motor,
            motorEletrico: embarcacaoEditarDTO.MotorEletrico,
            largura: embarcacaoEditarDTO.Largura,
            comprimento: embarcacaoEditarDTO.Comprimento,
            quantidadeDeLugar: embarcacaoEditarDTO.QuantidadeDeLugar);

        if (embarcacaoEditarDTO.GaleriaValida.Any())
        {
            var novasFotos = new List<GaleriaFotoEmbarcacao>();
            foreach (var item in embarcacaoEditarDTO.GaleriaValida)
            {
                var id = Guid.NewGuid();
                var url = item.Url;
                if (!url.StartsWith("http"))
                {
                    url = await _uploadImagemService.UploadAsync(item.Url, id);
                }
                novasFotos.Add(new GaleriaFotoEmbarcacao(id, url, embarcacao.Id));
            }
            await _embarcacaoRepository.AddGaleriaAsync(novasFotos);
        }

        if (embarcacaoEditarDTO.FotosExcluidas?.Count() > 0)
        {
            var fotosExcluidas = embarcacao.Galeria?
                .Where(g => embarcacaoEditarDTO.FotosExcluidas.Contains(g.Id))
                .ToList() ?? [];

            if (fotosExcluidas.Count > 0)
            {
                foreach (var item in fotosExcluidas)
                {
                    await _uploadImagemService.DeleteAsync(item.Id);
                }
                _embarcacaoRepository.RemoverGaleria(fotosExcluidas);
            }
        }

        _embarcacaoRepository.Editar(embarcacao);
        await _embarcacaoRepository.SaveChangesAsync();

        return (EmbarcacaoViewModel)embarcacao;
    }

    public async Task ExcluirAsync(Guid id)
    {
        var embarcacao = await ObterAsync(id);
        embarcacao.Excluir();
        _embarcacaoRepository.Editar(embarcacao);
        await _embarcacaoRepository.SaveChangesAsync();
    }

    public async Task<EmbarcacaoViewModel> ObterPorIdAsync(Guid id)
    {
        var embarcacao = await ObterAsync(id);
        return (EmbarcacaoViewModel)embarcacao;
    }

    public async Task<PaginacaoModel<EmbarcacaoViewModel>> PaginacaoAsync(FilterModel<Embarcacao> filtro)
    {
        filtro.GuiaDePescaId = _guiaDePescaLogado.Id;
        var paginacao = await _embarcacaoRepository.PaginacaoAsync(filtro);

        return new PaginacaoModel<EmbarcacaoViewModel>
        {
            Lista = paginacao.Lista.Select(e => (EmbarcacaoViewModel)e),
            QuantidadeDePaginas = paginacao.QuantidadeDePaginas,
        };
    }

    private async Task<Embarcacao> ObterAsync(Guid id)
    {
        return await _embarcacaoRepository.ObterPorIdAsync(id)
            ?? throw new ValidacaoException("Não foi possível localizar a embarcação");
    }
}

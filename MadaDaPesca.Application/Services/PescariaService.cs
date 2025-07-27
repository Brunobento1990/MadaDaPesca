using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Application.Services;

internal class PescariaService : IPescariaService
{
    private readonly IPescariaRepository _pescariaRepository;
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;
    private readonly IEmbarcacaoRepository _embarcacaoRepository;

    public PescariaService(IPescariaRepository pescariaRepository, IGuiaDePescaLogado guiaDePescaLogado, IEmbarcacaoRepository embarcacaoRepository)
    {
        _pescariaRepository = pescariaRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
        _embarcacaoRepository = embarcacaoRepository;
    }

    public async Task<PescariaViewModel> CriarAsync(PescariaDTO pescariaDTO)
    {
        Embarcacao? embarcacao = null;

        if (pescariaDTO.EmbarcacaoId.HasValue)
        {
            embarcacao = await _embarcacaoRepository.ObterPorIdAsync(pescariaDTO.EmbarcacaoId.Value)
                ?? throw new ValidacaoException("Embarcação não encontrada.");
        }

        var pescaria = Pescaria.Criar(
            titulo: pescariaDTO.Titulo,
            descricao: pescariaDTO.Descricao,
            local: pescariaDTO.Local,
            valor: pescariaDTO.Valor,
            quantidadePescador: pescariaDTO.QuantidadePescador,
            _guiaDePescaLogado.Id,
            horaInicial: pescariaDTO.HoraInicial,
            horaFinal: pescariaDTO.HoraFinal,
            quantidadeMaximaDeAgendamentosNoDia: pescariaDTO.QuantidadeMaximaDeAgendamentosNoDia,
            bloquearSegundaFeira: pescariaDTO.BloquearSegundaFeira,
            bloquearTercaFeira: pescariaDTO.BloquearTercaFeira,
            bloquearQuartaFeira: pescariaDTO.BloquearQuartaFeira,
            bloquearQuintaFeira: pescariaDTO.BloquearQuintaFeira,
            bloquearSextaFeira: pescariaDTO.BloquearSextaFeira,
            bloquearSabado: pescariaDTO.BloquearSabado,
            bloquearDomingo: pescariaDTO.BloquearDomingo,
            embarcacaoId: embarcacao?.Id);

        await _pescariaRepository.AddAsync(pescaria);
        await _pescariaRepository.SaveChangesAsync();

        return (PescariaViewModel)pescaria;
    }

    public async Task<PescariaViewModel> EditarAsync(PescariaEditarDTO pescariaEditarDTO)
    {
        var pescaria = await ObterAsync(pescariaEditarDTO.Id);
        Embarcacao? embarcacao = null;

        if (pescariaEditarDTO.EmbarcacaoId.HasValue)
        {
            embarcacao = await _embarcacaoRepository.ObterPorIdAsync(pescariaEditarDTO.EmbarcacaoId.Value)
                ?? throw new ValidacaoException("Embarcação não encontrada.");
        }

        pescaria.Editar(
            titulo: pescariaEditarDTO.Titulo,
            descricao: pescariaEditarDTO.Descricao,
            local: pescariaEditarDTO.Local,
            valor: pescariaEditarDTO.Valor,
            quantidadeDePescador: pescariaEditarDTO.QuantidadePescador,
            horaInicial: pescariaEditarDTO.HoraInicial,
            horaFinal: pescariaEditarDTO.HoraFinal,
            quantidadeMaximaDeAgendamentosNoDia: pescariaEditarDTO.QuantidadeMaximaDeAgendamentosNoDia,
            bloquearSegundaFeira: pescariaEditarDTO.BloquearSegundaFeira,
            bloquearTercaFeira: pescariaEditarDTO.BloquearTercaFeira,
            bloquearQuartaFeira: pescariaEditarDTO.BloquearQuartaFeira,
            bloquearQuintaFeira: pescariaEditarDTO.BloquearQuintaFeira,
            bloquearSextaFeira: pescariaEditarDTO.BloquearSextaFeira,
            bloquearSabado: pescariaEditarDTO.BloquearSabado,
            bloquearDomingo: pescariaEditarDTO.BloquearDomingo,
            embarcacaoId: embarcacao?.Id);

        _pescariaRepository.Editar(pescaria);
        await _pescariaRepository.SaveChangesAsync();

        return (PescariaViewModel)pescaria;
    }

    public async Task ExcluirAsync(Guid id)
    {
        var pescaria = await ObterAsync(id);
        pescaria.Excluir();
        _pescariaRepository.Editar(pescaria);
        await _pescariaRepository.SaveChangesAsync();
    }

    public async Task<PescariaViewModel> ObterPorIdAsync(Guid id)
    {
        var pescaria = await ObterAsync(id);

        return (PescariaViewModel)pescaria;
    }

    public async Task<PaginacaoModel<PescariaViewModel>> PaginacaoAsync(FilterModel<Pescaria> filtro)
    {
        filtro.GuiaDePescaId = _guiaDePescaLogado.Id;
        var paginacao = await _pescariaRepository.PaginacaoAsync(filtro);

        return new()
        {
            Lista = paginacao.Lista.Select(p => (PescariaViewModel)p),
            QuantidadeDePaginas = paginacao.QuantidadeDePaginas,
        };
    }

    private async Task<Pescaria> ObterAsync(Guid id)
    {
        return await _pescariaRepository.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Pescaria não encontrada.");
    }
}

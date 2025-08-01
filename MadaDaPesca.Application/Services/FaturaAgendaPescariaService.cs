using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class FaturaAgendaPescariaService : IFaturaAgendaPescariaService
{
    private readonly IFaturaAgendaPescariaRepository _faturaAgendaPescariaRepository;
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;

    public FaturaAgendaPescariaService(IFaturaAgendaPescariaRepository faturaAgendaPescariaRepository, IGuiaDePescaLogado guiaDePescaLogado)
    {
        _faturaAgendaPescariaRepository = faturaAgendaPescariaRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
    }

    public async Task<FaturaAgendaPescariaViewModel> GerarFaturaDaAgendaAsync(GerarFaturaAgendaPescariaDTO gerarFaturaAgendaPescariaDTO)
    {
        var fatura = await _faturaAgendaPescariaRepository.ObterFaturaDoAgendamentoAsync(gerarFaturaAgendaPescariaDTO.AgendaPescariaId, _guiaDePescaLogado.Id);
        if (fatura != null)
        {
            throw new ValidacaoException("Já existe uma fatura para a agenda selecionada");
        }

        fatura = new FaturaAgendaPescaria(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            agendaPescariaId: gerarFaturaAgendaPescariaDTO.AgendaPescariaId,
            dataDeVencimento: gerarFaturaAgendaPescariaDTO.DataDeVencimento,
            valor: gerarFaturaAgendaPescariaDTO.Valor,
            descricao: gerarFaturaAgendaPescariaDTO.Descricao,
            guiaDePescaId: _guiaDePescaLogado.Id);

        await _faturaAgendaPescariaRepository.AddAsync(fatura);
        await _faturaAgendaPescariaRepository.SaveChangesAsync();

        return (FaturaAgendaPescariaViewModel)fatura;
    }

    public async Task<FaturaAgendaPescariaViewModel> ObterFaturaDaAgendaAsync(Guid agendaPescariaId)
    {
        var fatura = await _faturaAgendaPescariaRepository.ObterFaturaDoAgendamentoAsync(agendaPescariaId, _guiaDePescaLogado.Id)
            ?? throw new ValidacaoException("Não foi possível localizar a fatura do agendamento selecionado");

        return (FaturaAgendaPescariaViewModel)fatura;
    }

    public async Task<FaturaAgendaPescariaViewModel> PagarFaturaDaAgendaAsync(PagarFaturaAgendaPescariaDTO pagarFaturaAgendaPescariaDTO)
    {
        var fatura = await _faturaAgendaPescariaRepository.ObterPorIdAsync(pagarFaturaAgendaPescariaDTO.Id, _guiaDePescaLogado.Id)
            ?? throw new ValidacaoException("Não foi possível localizar a fatura selecionada");

        var transacao = fatura.Pagar(
            pagarFaturaAgendaPescariaDTO.Valor,
            pagarFaturaAgendaPescariaDTO.Descricao,
            pagarFaturaAgendaPescariaDTO.MeioDePagamento);

        await _faturaAgendaPescariaRepository.AddTransacaoAsync(transacao);
        await _faturaAgendaPescariaRepository.SaveChangesAsync();

        return (FaturaAgendaPescariaViewModel)fatura;
    }
}

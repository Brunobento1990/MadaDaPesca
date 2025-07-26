using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class AgendaPescariaService : IAgendaPescariaService
{
    private readonly IAgendaPescariaRepository _agendaPescariaRepository;
    private readonly IPescariaRepository _pescariaRepository;
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;
    private readonly IUploadImagemService _uploadImagemService;

    public AgendaPescariaService(IAgendaPescariaRepository agendaPescariaRepository, IGuiaDePescaLogado guiaDePescaLogado, IPescariaRepository pescariaRepository, IUploadImagemService uploadImagemService)
    {
        _agendaPescariaRepository = agendaPescariaRepository;
        _guiaDePescaLogado = guiaDePescaLogado;
        _pescariaRepository = pescariaRepository;
        _uploadImagemService = uploadImagemService;
    }

    public async Task<IEnumerable<AgendaPescariaViewModel>> AgendaDoMesAsync(short mes, short ano)
    {
        var agendaDoMes = await _agendaPescariaRepository.ObterAgendaDaPescariaDoMesAsync(
            _guiaDePescaLogado.Id, mes, ano);

        return agendaDoMes.Select(agenda => (AgendaPescariaViewModel)agenda);
    }

    public async Task<AgendaPescariaViewModel> AgendarAsync(AgendarPescariaDTO agendarPescariaDTO)
    {
        var pescaria = await _pescariaRepository.ObterPorIdAsync(agendarPescariaDTO.PescariaId)
            ?? throw new ValidacaoException("Não foi possível localizar a pescaria selecionada");

        pescaria.EstaDisponivelParaAgendamento(agendarPescariaDTO.DataDeAgendamento);

        if (pescaria.QuantidadeMaximaDeAgendamentosNoDia.HasValue)
        {
            var agendamentos = await _agendaPescariaRepository
                .ObterAgendaDaPescariaDoDiaAsync(
                    agendarPescariaDTO.PescariaId,
                    (short)agendarPescariaDTO.DataDeAgendamento.Day,
                    (short)agendarPescariaDTO.DataDeAgendamento.Month,
                    (short)agendarPescariaDTO.DataDeAgendamento.Year);

            if (agendamentos.Where(x => x.Status != Domain.Enum.StatusAgendaPescariaEnum.Cancelada).Count() >= pescaria.QuantidadeMaximaDeAgendamentosNoDia)
            {
                throw new ValidacaoException("Quantidade máxima de agendamentos para o dia atingida.");
            }
        }

        if (pescaria.QuantidadePescador.HasValue &&
            agendarPescariaDTO.QuantidadeDePescador.HasValue &&
            agendarPescariaDTO.QuantidadeDePescador.Value > pescaria.QuantidadePescador.Value)
        {
            throw new ValidacaoException($"Quantidade máxima de pescador é de {pescaria.QuantidadePescador.Value}.");
        }


        var agendamento = AgendaPescaria.Criar(
            observacao: agendarPescariaDTO.Observacao,
            pescariaId: pescaria.Id,
            status: agendarPescariaDTO.Status,
            dia: (short)agendarPescariaDTO.DataDeAgendamento.Day,
            mes: (short)agendarPescariaDTO.DataDeAgendamento.Month,
            ano: (short)agendarPescariaDTO.DataDeAgendamento.Year,
            horaInicial: agendarPescariaDTO.HoraInicial ?? pescaria.HoraInicial,
            horaFinal: agendarPescariaDTO.HoraFinal ?? pescaria.HoraFinal,
            quantidadeDePescador: agendarPescariaDTO.QuantidadeDePescador ?? pescaria.QuantidadePescador);

        if (agendarPescariaDTO.Galeria != null && agendarPescariaDTO.Galeria.Any())
        {
            foreach (var foto in agendarPescariaDTO.Galeria)
            {
                var id = Guid.NewGuid();
                var url = await _uploadImagemService.UploadAsync(foto.Url, id);

                agendamento.Galeria.Add(new GaleriaAgendaPescaria(
                    id: id,
                    url: url,
                    agendaPescariaId: agendamento.Id));
            }
        }

        await _agendaPescariaRepository.AddAsync(agendamento);
        await _agendaPescariaRepository.SaveChangesAsync();

        var agendaViewModel = (AgendaPescariaViewModel)agendamento;
        agendaViewModel.Pescaria = (PescariaViewModel)pescaria;

        return agendaViewModel;
    }

    public async Task<AgendaPescariaViewModel> EditarAsync(EditarAgendaPescariaDTO editarAgendaPescariaDTO)
    {
        var pescaria = await _pescariaRepository.ObterPorIdAsync(editarAgendaPescariaDTO.PescariaId)
            ?? throw new ValidacaoException("Não foi possível localizar a pescaria selecionada");
        var agenda = await _agendaPescariaRepository.ObterPorIdAsync(editarAgendaPescariaDTO.AgendaPescariaId)
            ?? throw new ValidacaoException("Não foi possível localizar o agendamento selecionado");

        agenda.Editar(editarAgendaPescariaDTO.Observacao,
            editarAgendaPescariaDTO.Status ?? agenda.Status,
            (short)editarAgendaPescariaDTO.DataDeAgendamento.Day,
            (short)editarAgendaPescariaDTO.DataDeAgendamento.Month,
            (short)editarAgendaPescariaDTO.DataDeAgendamento.Year,
            editarAgendaPescariaDTO.HoraInicial,
            editarAgendaPescariaDTO.HoraFinal,
            editarAgendaPescariaDTO.QuantidadeDePescador,
            pescariaId: pescaria.Id);

        if (editarAgendaPescariaDTO.Galeria != null && editarAgendaPescariaDTO.Galeria.Any())
        {
            var galeria = new List<GaleriaAgendaPescaria>();

            foreach (var foto in editarAgendaPescariaDTO.Galeria)
            {
                var id = Guid.NewGuid();
                var url = await _uploadImagemService.UploadAsync(foto.Url, id);
                galeria.Add(new GaleriaAgendaPescaria(
                    id: id,
                    url: url,
                    agendaPescariaId: agenda.Id));
            }

            await _agendaPescariaRepository.AddGaleriaAsync(galeria);
        }

        _agendaPescariaRepository.Editar(agenda);
        await _agendaPescariaRepository.SaveChangesAsync();

        return (AgendaPescariaViewModel)agenda;
    }

    public async Task<AgendaPescariaViewModel> ObterPorIdAsync(Guid id)
    {
        var agenda = await _agendaPescariaRepository.ObterPorIdAsync(id)
            ?? throw new ValidacaoException("Não foi possível localizar o agendamento selecionado");

        return (AgendaPescariaViewModel)agenda;
    }
}

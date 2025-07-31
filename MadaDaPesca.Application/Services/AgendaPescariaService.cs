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

    public async Task<AgendaDoMesViewModel> AgendaDoMesAsync(short mes, short ano)
    {
        var agendaDoMes = await _agendaPescariaRepository.ObterAgendaDaPescariaDoMesAsync(
            _guiaDePescaLogado.Id, mes, ano);

        var agenda = agendaDoMes.Select(agenda => (AgendaPescariaViewModel)agenda);
        var todasDatasBloqueadas = await _pescariaRepository.ObterTodasDatasBloqueadasAsync(mes: mes, ano: ano, guiaDePescaId: _guiaDePescaLogado.Id);

        return new()
        {
            Agenda = agenda,
            AgendaBloqueada = todasDatasBloqueadas.Select(x => (BloqueioDataPescariaViewModel)x)
        };
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
        var agenda = await _agendaPescariaRepository.ObterPorIdAsync(editarAgendaPescariaDTO.Id)
            ?? throw new ValidacaoException("Não foi possível localizar o agendamento selecionado");

        agenda.Editar(editarAgendaPescariaDTO.Observacao,
            editarAgendaPescariaDTO.Status ?? agenda.Status,
            editarAgendaPescariaDTO.HoraInicial,
            editarAgendaPescariaDTO.HoraFinal,
            editarAgendaPescariaDTO.QuantidadeDePescador,
            pescariaId: pescaria.Id);

        if (editarAgendaPescariaDTO.FotosAdicionas.Any())
        {
            var galeria = new List<GaleriaAgendaPescaria>();

            foreach (var foto in editarAgendaPescariaDTO.FotosAdicionas)
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

        if (editarAgendaPescariaDTO.FotosExcluidas?.Any() ?? false)
        {
            var fotosExcluidas = agenda.Galeria
                .Where(x => editarAgendaPescariaDTO.FotosExcluidas.Contains(x.Id))
                .ToList();

            if (fotosExcluidas.Count > 0)
            {
                foreach (var item in fotosExcluidas)
                {
                    await _uploadImagemService.DeleteAsync(item.Id);
                }

                _agendaPescariaRepository.ExcluirGaleria(fotosExcluidas);
            }
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

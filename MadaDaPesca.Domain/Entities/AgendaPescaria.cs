
using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Domain.Entities;

public sealed class AgendaPescaria : BaseEntity
{
    public AgendaPescaria(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        string? observacao,
        Guid pescariaId,
        StatusAgendaPescariaEnum status,
        short dia,
        short mes,
        short ano,
        short? horaInicial,
        short? horaFinal,
        short? quantidadeDePescador)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        Observacao = observacao;
        PescariaId = pescariaId;
        Status = status;
        Dia = dia;
        Mes = mes;
        Ano = ano;
        HoraInicial = horaInicial;
        HoraFinal = horaFinal;
        QuantidadeDePescador = quantidadeDePescador;
    }

    public short Dia { get; private set; }
    public short Mes { get; private set; }
    public short Ano { get; private set; }
    public short? HoraInicial { get; private set; }
    public short? HoraFinal { get; private set; }
    public short? QuantidadeDePescador { get; private set; }
    public string? Observacao { get; private set; }
    public StatusAgendaPescariaEnum Status { get; private set; }
    public Guid PescariaId { get; private set; }
    public Pescaria Pescaria { get; set; } = null!;
    public IList<GaleriaAgendaPescaria> Galeria { get; set; } = [];

    public void Excluir()
    {
        Excluido = true;
        DataDeAtualizacao = DateTime.UtcNow;
    }

    public static AgendaPescaria Criar(
        string? observacao,
        Guid pescariaId,
        StatusAgendaPescariaEnum? status,
        short dia,
        short mes,
        short ano,
        short? horaInicial,
        short? horaFinal,
        short? quantidadeDePescador
        )
    {
        var agendaPescaria = new AgendaPescaria(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            observacao: observacao,
            pescariaId: pescariaId,
            status: status ?? StatusAgendaPescariaEnum.Pendente,
            dia: dia,
            mes: mes,
            ano: ano,
            horaInicial: horaInicial,
            horaFinal: horaFinal,
            quantidadeDePescador: quantidadeDePescador);

        return agendaPescaria;
    }

    public void Editar(
        string? observacao,
        StatusAgendaPescariaEnum status,
        short? horaInicial,
        short? horaFinal,
        short? quantidadeDePescador,
        Guid pescariaId)
    {
        Observacao = observacao;
        Status = status;
        HoraInicial = horaInicial;
        HoraFinal = horaFinal;
        DataDeAtualizacao = DateTime.UtcNow;
        QuantidadeDePescador = quantidadeDePescador;
        PescariaId = pescariaId;
    }

    public void Reagendar(DateTime dataDeAgendamento)
    {
        Pescaria.EstaDisponivelParaAgendamento(dataDeAgendamento);

        Dia = (short)dataDeAgendamento.Day;
        Mes = (short)dataDeAgendamento.Month;
        Ano = (short)dataDeAgendamento.Year;

        DataDeAtualizacao = DateTime.UtcNow;
    }
}

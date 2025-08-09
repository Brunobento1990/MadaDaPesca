
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
    public FaturaAgendaPescaria? FaturaAgendaPescaria { get; set; }

    public void Excluir()
    {
        Excluido = true;
        DataDeAtualizacao = DateTime.Now;
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
        short? quantidadeDePescador,
        decimal valor,
        Guid guiaDePescaId,
        decimal? valorDaFatura,
        DateTime? dataDeVencimento,
        decimal? valorDeEntrada
        )
    {
        var agendaPescaria = new AgendaPescaria(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.Now,
            dataDeAtualizacao: DateTime.Now,
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

        agendaPescaria.FaturaAgendaPescaria = new FaturaAgendaPescaria(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.Now,
            dataDeAtualizacao: DateTime.Now,
            excluido: false,
            agendaPescariaId: agendaPescaria.Id,
            dataDeVencimento: dataDeVencimento.HasValue && dataDeVencimento.Value != default ? dataDeVencimento.Value : new DateTime(year: ano, month: mes, day: dia).Date,
            valor: valorDaFatura.HasValue && valorDaFatura.Value > 0 ? valorDaFatura.Value : valor,
            descricao: $"Agendamento para data: {dia.ToString().PadLeft(2, '0')}/{mes.ToString().PadLeft(2, '0')}/{ano.ToString().PadLeft(2, '0')}",
            guiaDePescaId: guiaDePescaId);

        if (valorDeEntrada.HasValue && valorDeEntrada.Value > 0)
        {
            agendaPescaria.FaturaAgendaPescaria.Transacoes = [];
            agendaPescaria.FaturaAgendaPescaria.Transacoes.Add(agendaPescaria.FaturaAgendaPescaria.Pagar(
                valor: valorDeEntrada.Value,
                descricao: "Pagamento de entrada no agendamento",
                meioDePagamentoEnum: MeioDePagamentoEnum.Dinheiro));
        }

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
        DataDeAtualizacao = DateTime.Now;
        QuantidadeDePescador = quantidadeDePescador;
        PescariaId = pescariaId;
    }

    public void Reagendar(DateTime dataDeAgendamento)
    {
        Pescaria.EstaDisponivelParaAgendamento(dataDeAgendamento);

        Dia = (short)dataDeAgendamento.Day;
        Mes = (short)dataDeAgendamento.Month;
        Ano = (short)dataDeAgendamento.Year;

        DataDeAtualizacao = DateTime.Now;
    }
}


using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Domain.Entities;

public sealed class TransacaoFaturaAgendaPescaria : BaseEntity
{
    public TransacaoFaturaAgendaPescaria(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        Guid faturaAgendaPescariaId,
        decimal valor,
        string? descricao,
        MeioDePagamentoEnum meioDePagamento,
        TipoTransacaoEnum tipoTransacao)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        FaturaAgendaPescariaId = faturaAgendaPescariaId;
        Valor = valor;
        Descricao = descricao;
        MeioDePagamento = meioDePagamento;
        TipoTransacao = tipoTransacao;
    }

    public Guid FaturaAgendaPescariaId { get; private set; }
    public FaturaAgendaPescaria FaturaAgendaPescaria { get; set; } = null!;
    public decimal Valor { get; private set; }
    public string? Descricao { get; private set; }
    public MeioDePagamentoEnum MeioDePagamento { get; private set; }
    public TipoTransacaoEnum TipoTransacao { get; private set; }
}

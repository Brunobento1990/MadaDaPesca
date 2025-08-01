
using MadaDaPesca.Domain.Enum;
using MadaDaPesca.Domain.Exceptions;

namespace MadaDaPesca.Domain.Entities;

public class FaturaAgendaPescaria : BaseEntity
{
    public FaturaAgendaPescaria(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        Guid agendaPescariaId,
        DateTime dataDeVencimento,
        decimal valor,
        string? descricao,
        Guid guiaDePescaId)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        AgendaPescariaId = agendaPescariaId;
        DataDeVencimento = dataDeVencimento;
        Valor = valor;
        Descricao = descricao;
        GuiaDePescaId = guiaDePescaId;
    }

    public Guid AgendaPescariaId { get; private set; }
    public AgendaPescaria AgendaPescaria { get; set; } = null!;
    public Guid GuiaDePescaId { get; private set; }
    public GuiaDePesca GuiaDePesca { get; set; } = null!;
    public DateTime DataDeVencimento { get; private set; }
    public decimal Valor { get; private set; }
    public decimal? ValorRecebido => Transacoes?.Where(x => x.TipoTransacao == Enum.TipoTransacaoEnum.Entrada)?.Sum(x => x.Valor);
    public decimal? ValorAReceber => !ValorRecebido.HasValue ? null : Valor - ValorRecebido.Value;
    public string? Descricao { get; private set; }
    public bool Vencida => DataDeVencimento.Date < DateTime.Now.Date && (ValorRecebido ?? 0) < Valor;
    public bool Quitada => (ValorRecebido ?? 0) >= Valor;

    public IList<TransacaoFaturaAgendaPescaria>? Transacoes { get; set; }

    public TransacaoFaturaAgendaPescaria Pagar(decimal valor, string? descricao, MeioDePagamentoEnum meioDePagamentoEnum)
    {
        if (Quitada)
        {
            throw new ValidacaoException("A fatura já está quitada");
        }
        if (valor <= 0)
        {
            throw new ValidacaoException("O valor do pagamento deve ser maior que zero");
        }
        if (valor > ValorAReceber)
        {
            throw new ValidacaoException("O valor do pagamento não pode ser maior que o valor a receber");
        }

        return new TransacaoFaturaAgendaPescaria(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            faturaAgendaPescariaId: Id,
            valor: valor,
            descricao: descricao,
            meioDePagamento: meioDePagamentoEnum,
            tipoTransacao: TipoTransacaoEnum.Entrada);
    }
}

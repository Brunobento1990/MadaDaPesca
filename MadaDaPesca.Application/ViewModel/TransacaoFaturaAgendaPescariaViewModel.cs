using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Application.ViewModel;

public class TransacaoFaturaAgendaPescariaViewModel : BaseViewModel
{
    public Guid FaturaAgendaPescariaId { get; set; }
    public decimal Valor { get; set; }
    public string? Descricao { get; set; }
    public MeioDePagamentoEnum MeioDePagamento { get; set; }
    public TipoTransacaoEnum TipoTransacao { get; set; }

    public static explicit operator TransacaoFaturaAgendaPescariaViewModel(TransacaoFaturaAgendaPescaria transacao)
    {
        return new TransacaoFaturaAgendaPescariaViewModel
        {
            DataDeAtualizacao = transacao.DataDeAtualizacao,
            DataDeCadastro = transacao.DataDeCadastro,
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            FaturaAgendaPescariaId = transacao.FaturaAgendaPescariaId,
            MeioDePagamento = transacao.MeioDePagamento,
            TipoTransacao = transacao.TipoTransacao,
            Valor = transacao.Valor
        };
    }
}

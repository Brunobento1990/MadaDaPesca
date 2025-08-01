using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class FaturaAgendaPescariaViewModel : BaseViewModel
{
    public Guid AgendaPescariaId { get; set; }
    public AgendaPescariaViewModel AgendaPescaria { get; set; } = null!;
    public Guid GuiaDePescaId { get; set; }
    public GuiaDePescaViewModel GuiaDePesca { get; set; } = null!;
    public DateTime DataDeVencimento { get; set; }
    public decimal Valor { get; set; }
    public decimal? ValorRecebido { get; set; }
    public decimal? ValorAReceber { get; set; }
    public string? Descricao { get; set; }
    public bool Vencida { get; set; }
    public bool Quitada { get; set; }
    public IEnumerable<TransacaoFaturaAgendaPescariaViewModel>? Transacoes { get; set; }

    public static explicit operator FaturaAgendaPescariaViewModel(FaturaAgendaPescaria fatura)
    {
        return new FaturaAgendaPescariaViewModel
        {
            DataDeAtualizacao = fatura.DataDeAtualizacao,
            DataDeCadastro = fatura.DataDeCadastro,
            Id = fatura.Id,
            Descricao = fatura.Descricao,
            AgendaPescariaId = fatura.AgendaPescariaId,
            DataDeVencimento = fatura.DataDeVencimento,
            Quitada = fatura.Quitada,
            Valor = fatura.Valor,
            Vencida = fatura.Vencida,
            ValorRecebido = fatura.ValorRecebido,
            ValorAReceber = fatura.ValorAReceber,
            Transacoes = fatura.Transacoes?.Select(x => (TransacaoFaturaAgendaPescariaViewModel)x),
            AgendaPescaria = fatura.AgendaPescaria == null ? null! : (AgendaPescariaViewModel)fatura.AgendaPescaria,
            GuiaDePesca = fatura.GuiaDePesca == null ? null! : (GuiaDePescaViewModel)fatura.GuiaDePesca,
            GuiaDePescaId = fatura.GuiaDePescaId
        };
    }
}
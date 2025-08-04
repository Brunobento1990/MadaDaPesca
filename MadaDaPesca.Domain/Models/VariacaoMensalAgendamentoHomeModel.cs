namespace MadaDaPesca.Domain.Models;

public class VariacaoMensalAgendamentoHomeModel
{
    public int Mes { get; set; }
    public int AnoAnterior { get; set; }
    public int AnoAtual { get; set; }
    public int TotalAnoAnterior { get; set; }
    public int TotalAnoAtual { get; set; }
    public decimal Porcentagem { get; set; }
}

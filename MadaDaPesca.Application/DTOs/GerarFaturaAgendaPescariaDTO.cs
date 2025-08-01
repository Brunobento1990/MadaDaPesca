namespace MadaDaPesca.Application.DTOs;

public class GerarFaturaAgendaPescariaDTO
{
    public Guid AgendaPescariaId { get; set; }
    public decimal Valor { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataDeVencimento { get; set; }
}

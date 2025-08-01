using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Application.DTOs;

public class PagarFaturaAgendaPescariaDTO
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public string? Descricao { get; set; }
    public MeioDePagamentoEnum MeioDePagamento { get; set; }
}

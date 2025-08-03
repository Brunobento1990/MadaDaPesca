using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Application.DTOs;

public class EstornarFaturaAgendaPescariaDTO
{
    public Guid Id { get; set; }
    public string? Descricao { get; set; }
}

using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Application.DTOs;

public class AgendarPescariaDTO
{
    public Guid PescariaId { get; set; }
    public short? QuantidadeDePescador { get; set; }
    public DateTime DataDeAgendamento { get; set; }
    public string? Observacao { get; set; }
    public virtual StatusAgendaPescariaEnum? Status { get; set; }
    public short? HoraInicial { get; set; }
    public short? HoraFinal { get; set; }
    public IList<GaleriaAgendaPescariaDTO>? Galeria { get; set; }
}

public class GaleriaAgendaPescariaDTO
{
    public string Url { get; set; } = string.Empty;
}

public class EditarAgendaPescariaDTO : AgendarPescariaDTO
{
    public Guid AgendaPescariaId { get; set; }
}

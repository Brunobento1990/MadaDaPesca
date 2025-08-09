using MadaDaPesca.Domain.Enum;
using System.Text.Json.Serialization;

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
    public FaturaAgendaCriarDTO? Fatura { get; set; }
}

public class FaturaAgendaCriarDTO
{
    public DateTime? DataDeVencimento { get; set; }
    public decimal? Valor { get; set; }
    public decimal? ValorDeEntrada { get; set; }
}

public class GaleriaAgendaPescariaDTO
{
    public Guid? Id { get; set; }
    public string Url { get; set; } = string.Empty;
}

public class EditarAgendaPescariaDTO : AgendarPescariaDTO
{
    public Guid Id { get; set; }
    public IList<GaleriaAgendaPescariaDTO>? Galeria { get; set; }
    public IList<Guid>? FotosExcluidas { get; set; }
    [JsonIgnore]
    public IEnumerable<GaleriaAgendaPescariaDTO> FotosAdicionas => Galeria?.Where(x => !x.Id.HasValue && !x.Url.StartsWith("http")) ?? [];
}

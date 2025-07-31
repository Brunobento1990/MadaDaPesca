using System.Text.Json.Serialization;

namespace MadaDaPesca.Application.DTOs;

public class EmbarcacaoCreateDTO
{
    public string Nome { get; set; } = string.Empty;
    public string? Motor { get; set; }
    public string? MotorEletrico { get; set; }
    public string? Largura { get; set; }
    public string? Comprimento { get; set; }
    public short? QuantidadeDeLugar { get; set; }
    public IList<GaleriaFotoEmbarcacaoDTO>? Galeria { get; set; }
    [JsonIgnore]
    public IEnumerable<GaleriaFotoEmbarcacaoDTO> GaleriaValida => Galeria?.Where(x => !string.IsNullOrWhiteSpace(x.Url) && !x.Id.HasValue) ?? [];
}

public class GaleriaFotoEmbarcacaoDTO
{
    public Guid? Id { get; set; }
    public string Url { get; set; } = string.Empty;
}


public class EmbarcacaoEditarDTO : EmbarcacaoCreateDTO
{
    public Guid Id { get; set; }
    public IEnumerable<Guid>? FotosExcluidas { get; set; }
}

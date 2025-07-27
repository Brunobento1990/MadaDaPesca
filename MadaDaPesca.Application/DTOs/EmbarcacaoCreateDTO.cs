namespace MadaDaPesca.Application.DTOs;

public class EmbarcacaoCreateDTO
{
    public string Nome { get; set; } = string.Empty;
    public string? Motor { get; set; }
    public string? MotorEletrico { get; set; }
    public string? Largura { get; set; }
    public string? Comprimento { get; set; }
    public short? QuantidadeDeLugar { get; set; }
}

public class EmbarcacaoEditarDTO : EmbarcacaoCreateDTO
{
    public Guid Id { get; set; }
}

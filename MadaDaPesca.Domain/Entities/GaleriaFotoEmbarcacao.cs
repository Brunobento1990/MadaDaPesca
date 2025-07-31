namespace MadaDaPesca.Domain.Entities;

public sealed class GaleriaFotoEmbarcacao
{
    public GaleriaFotoEmbarcacao(Guid id, string url, Guid embarcacaoId)
    {
        Id = id;
        Url = url;
        EmbarcacaoId = embarcacaoId;
    }

    public Guid Id { get; private set; }
    public string Url { get; private set; }
    public Guid EmbarcacaoId { get; private set; }
    public Embarcacao Embarcacao { get; set; } = null!;
}

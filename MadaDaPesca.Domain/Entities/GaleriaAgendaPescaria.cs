namespace MadaDaPesca.Domain.Entities;

public sealed class GaleriaAgendaPescaria
{
    public GaleriaAgendaPescaria(Guid id, string url, Guid agendaPescariaId)
    {
        Id = id;
        Url = url;
        AgendaPescariaId = agendaPescariaId;
    }

    public Guid Id { get; private set; }
    public string Url { get; private set; }
    public Guid AgendaPescariaId { get; private set; }
    public AgendaPescaria AgendaPescaria { get; set; } = null!;
}

namespace MadaDaPesca.Domain.Entities;

public sealed class BloqueioDataPescaria
{
    public BloqueioDataPescaria(Guid id, DateTime data, Guid pescariaId)
    {
        Id = id;
        Data = data;
        PescariaId = pescariaId;
    }

    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }
    public Guid PescariaId { get; private set; }
    public Pescaria Pescaria { get; set; } = null!;
}

namespace MadaDaPesca.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(Guid id, DateTime dataDeCadastro, DateTime dataDeAtualizacao, bool excluido)
    {
        Id = id;
        DataDeCadastro = dataDeCadastro;
        DataDeAtualizacao = dataDeAtualizacao;
        Excluido = excluido;
    }

    public Guid Id { get; protected set; }
    public DateTime DataDeCadastro { get; protected set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; protected set; } = DateTime.Now;
    public bool Excluido { get; protected set; } = false;
}

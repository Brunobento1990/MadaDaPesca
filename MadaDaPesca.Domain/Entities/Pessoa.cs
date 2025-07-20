
namespace MadaDaPesca.Domain.Entities;

public sealed class Pessoa : BaseEntity
{
    public Pessoa(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        string cpf,
        string nome,
        string telefone,
        string email)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        Cpf = cpf;
        Nome = nome;
        Telefone = telefone;
        Email = email;
    }

    public string Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
}

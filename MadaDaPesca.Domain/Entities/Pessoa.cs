
using MadaDaPesca.Domain.Extensions;

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
        string email,
        string? urlFoto,
        Guid? idFoto)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        Cpf = cpf;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        UrlFoto = urlFoto;
        IdFoto = idFoto;
    }

    public string Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public string? UrlFoto { get; private set; }
    public Guid? IdFoto { get; private set; }

    public void EditarFoto(string urlFoto, Guid idFoto)
    {
        UrlFoto = urlFoto;
        IdFoto = idFoto;
    }

    public void Editar(
        string nome,
        string email,
        string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;

        Validar();
    }

    public void Validar()
    {
        Cpf = Cpf.ValidarNull("Informe o CPF")
            .ValidarLength(11, "O CPF deve conter no máximo 11 caracteres");
        Nome = Nome.ValidarNull("Informe o nome")
            .ValidarLength(100, "O nome deve conter no máximo 100 caracteres");
        Telefone = Telefone.ValidarNull("Informe o telefone")
            .ValidarLength(11, "O telefone deve conter no máximo 11 caracteres");
        Email = Email.ValidarNull("Informe o e-mail")
            .ValidarLength(255, "O e-mail deve conter no máximo 255 caracteres");
    }
}

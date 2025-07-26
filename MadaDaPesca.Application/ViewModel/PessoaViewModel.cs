using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class PessoaViewModel : BaseViewModel
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? UrlFoto { get; set; }

    public static explicit operator PessoaViewModel(Pessoa pessoa)
    {
        return new PessoaViewModel
        {
            Id = pessoa.Id,
            DataDeCadastro = pessoa.DataDeCadastro,
            DataDeAtualizacao = pessoa.DataDeAtualizacao,
            Nome = pessoa.Nome,
            Cpf = pessoa.Cpf,
            Telefone = pessoa.Telefone,
            Email = pessoa.Email,
            UrlFoto = pessoa.UrlFoto
        };
    }
}

using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;
using System;

namespace MadaDaPesca.Domain.Entities;

public sealed class GuiaDePesca : BaseEntity
{
    public GuiaDePesca(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        Guid pessoaId,
        Guid acessoGuiaDePescaId,
        bool aceitoDeTermos)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        PessoaId = pessoaId;
        AcessoGuiaDePescaId = acessoGuiaDePescaId;
        AceitoDeTermos = aceitoDeTermos;
    }

    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; set; } = null!;
    public Guid AcessoGuiaDePescaId { get; private set; }
    public bool AceitoDeTermos { get; private set; }
    public AcessoGuiaDePesca AcessoGuiaDePesca { get; set; } = null!;
    public IList<Pescaria>? Pescarias { get; set; }
    public IList<Embarcacao>? Embarcacoes { get; set; }

    public static GuiaDePesca Novo(
        string cpf,
        string nome,
        string telefone,
        string email,
        string senha,
        string? urlFoto,
        Guid id,
        bool aceitoDeTermos)
    {
        cpf.ValidarNull("Informe o CPF")
            .ValidarLength(11, "O CPF deve conter no máximo 11 caracteres");
        nome.ValidarNull("Informe o nome")
            .ValidarLength(100, "O nome deve conter no máximo 100 caracteres");
        telefone.ValidarNull("Informe o telefone")
            .ValidarLength(11, "O telefone deve conter no máximo 11 caracteres");
        email.ValidarNull("Informe o e-mail")
            .ValidarLength(255, "O e-mail deve conter no máximo 255 caracteres");
        senha.ValidarNull("Informe a senha");

        var acessoGuiaDePesca = new AcessoGuiaDePesca(
            id: id,
            senha: senha,
            primeiroAcesso: true,
            emailVerificado: false,
            tokenEsqueceuSenha: null,
            expiracaoTokenEsqueceuSenha: null,
            acessoBloqueado: false,
            trocouSenha: null);

        var pessoa = new Pessoa(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            cpf: cpf,
            nome: nome,
            telefone: telefone,
            email: email,
            urlFoto: urlFoto);

        return new GuiaDePesca(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            pessoaId: pessoa.Id,
            acessoGuiaDePescaId: acessoGuiaDePesca.Id,
            aceitoDeTermos: aceitoDeTermos)
        {
            AcessoGuiaDePesca = acessoGuiaDePesca,
            Pessoa = pessoa
        };
    }

    public void ValidarAcesso()
    {
        if (AcessoGuiaDePesca.AcessoBloqueado)
        {
            throw new ValidacaoException("Seu acesso está bloqueado, entre em contato com o suporte", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }

        if (Excluido)
        {
            throw new ValidacaoException("Seu acesso está excluído, entre em contato com o suporte", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }

        if (!AcessoGuiaDePesca.EmailVerificado)
        {
            throw new ValidacaoException("Seu e-mail não foi verificado, verifique sua caixa de entrada ou spam", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }

        if (!AceitoDeTermos)
        {
            throw new ValidacaoException("Você deve aceitar os termos de uso para acessar a plataforma", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }
    }
}

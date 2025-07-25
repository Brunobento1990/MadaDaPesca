﻿using MadaDaPesca.Domain.Extensions;

namespace MadaDaPesca.Domain.Entities;

public sealed class GuiaDePesca : BaseEntity
{
    public GuiaDePesca(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        Guid pessoaId,
        Guid acessoGuiaDePescaId)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        PessoaId = pessoaId;
        AcessoGuiaDePescaId = acessoGuiaDePescaId;
    }

    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; set; } = null!;
    public Guid AcessoGuiaDePescaId { get; private set; }
    public AcessoGuiaDePesca AcessoGuiaDePesca { get; set; } = null!;
    public IList<Pescaria>? Pescarias { get; set; }

    public static GuiaDePesca Novo(
        string cpf,
        string nome,
        string telefone,
        string email,
        string senha)
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
            id: Guid.NewGuid(),
            senha: senha,
            primeiroAcesso: true,
            emailVerificado: false,
            tokenEsqueceuSenha: null,
            expiracaoTokenEsqueceuSenha: null,
            acessoBloqueado: false);

        var pessoa = new Pessoa(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            cpf: cpf,
            nome: nome,
            telefone: telefone,
            email: email);

        return new GuiaDePesca(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            pessoaId: pessoa.Id,
            acessoGuiaDePescaId: acessoGuiaDePesca.Id)
        {
            AcessoGuiaDePesca = acessoGuiaDePesca,
            Pessoa = pessoa
        };
    }
}

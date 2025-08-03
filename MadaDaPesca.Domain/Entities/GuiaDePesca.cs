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
        bool aceitoDeTermos,
        Guid? idFoto)
    {
        var acessoGuiaDePesca = new AcessoGuiaDePesca(
            id: Guid.NewGuid(),
            senha: senha,
            primeiroAcesso: true,
            emailVerificado: false,
            tokenEsqueceuSenha: null,
            expiracaoTokenEsqueceuSenha: null,
            acessoBloqueado: false,
            trocouSenha: null);

        var pessoa = new Pessoa(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.Now,
            dataDeAtualizacao: DateTime.Now,
            excluido: false,
            cpf: cpf,
            nome: nome,
            telefone: telefone,
            email: email,
            urlFoto: urlFoto,
            idFoto: idFoto);

        pessoa.Validar();

        return new GuiaDePesca(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.Now,
            dataDeAtualizacao: DateTime.Now,
            excluido: false,
            pessoaId: pessoa.Id,
            acessoGuiaDePescaId: acessoGuiaDePesca.Id,
            aceitoDeTermos: aceitoDeTermos)
        {
            AcessoGuiaDePesca = acessoGuiaDePesca,
            Pessoa = pessoa
        };
    }

    public void Editar(
        string nome,
        string email,
        string telefone)
    {
        Pessoa.Editar(
            nome: nome,
            email: email,
            telefone: telefone);

        DataDeAtualizacao = DateTime.Now;
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

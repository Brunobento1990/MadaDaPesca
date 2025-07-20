namespace MadaDaPesca.Domain.Entities;

public sealed class AcessoGuiaDePesca
{

    public AcessoGuiaDePesca(
        Guid id,
        string senha,
        bool primeiroAcesso,
        bool emailVerificado,
        Guid? tokenEsqueceuSenha,
        DateTime? expiracaoTokenEsqueceuSenha,
        bool acessoBloqueado)
    {
        Id = id;
        Senha = senha;
        PrimeiroAcesso = primeiroAcesso;
        EmailVerificado = emailVerificado;
        TokenEsqueceuSenha = tokenEsqueceuSenha;
        ExpiracaoTokenEsqueceuSenha = expiracaoTokenEsqueceuSenha;
        AcessoBloqueado = acessoBloqueado;
    }
    public Guid Id { get; private set; }
    public string Senha { get; private set; }
    public bool PrimeiroAcesso { get; private set; }
    public bool EmailVerificado { get; private set; }
    public bool AcessoBloqueado { get; private set; }
    public Guid? TokenEsqueceuSenha { get; private set; }
    public DateTime? ExpiracaoTokenEsqueceuSenha { get; private set; }
}

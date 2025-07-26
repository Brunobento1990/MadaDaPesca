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
        bool acessoBloqueado,
        DateTime? trocouSenha)
    {
        Id = id;
        Senha = senha;
        PrimeiroAcesso = primeiroAcesso;
        EmailVerificado = emailVerificado;
        TokenEsqueceuSenha = tokenEsqueceuSenha;
        ExpiracaoTokenEsqueceuSenha = expiracaoTokenEsqueceuSenha;
        AcessoBloqueado = acessoBloqueado;
        TrocouSenha = trocouSenha;
    }
    public Guid Id { get; private set; }
    public string Senha { get; private set; }
    public bool PrimeiroAcesso { get; private set; }
    public bool EmailVerificado { get; private set; }
    public bool AcessoBloqueado { get; private set; }
    public Guid? TokenEsqueceuSenha { get; private set; }
    public DateTime? ExpiracaoTokenEsqueceuSenha { get; private set; }
    public DateTime? TrocouSenha { get; private set; }

    public void EsqueceuSenha()
    {
        TokenEsqueceuSenha = Guid.NewGuid();
        ExpiracaoTokenEsqueceuSenha = DateTime.UtcNow.AddHours(1);
    }

    public void RecuperarSenha(string senha)
    {
        Senha = senha;
        PrimeiroAcesso = false;
        TokenEsqueceuSenha = null;
        ExpiracaoTokenEsqueceuSenha = null;
        TrocouSenha = DateTime.UtcNow;
    }

    public void VerificarEmail()
    {
        EmailVerificado = true;
    }
}

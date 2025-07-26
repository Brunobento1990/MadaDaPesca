using MadaDaPesca.Domain.Exceptions;

namespace MadaDaPesca.Application.DTOs;

public class RecuperarSenhaDTO
{
    public string Senha { get; set; } = string.Empty;
    public string ReSenha { get; set; } = string.Empty;
    public Guid TokenEsqueceuSenha { get; set; }

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Senha))
        {
            throw new ValidacaoException("Informe a senha");
        }

        if (string.IsNullOrWhiteSpace(ReSenha))
        {
            throw new ValidacaoException("Confirme a senha");
        }

        if (!Senha.Equals(ReSenha))
        {
            throw new ValidacaoException("As senha não conferem");
        }

        if (TokenEsqueceuSenha == Guid.Empty)
        {
            throw new ValidacaoException("Token inválido");
        }
    }
}

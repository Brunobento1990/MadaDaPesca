namespace MadaDaPesca.Domain.Exceptions;

public class ValidacaoException : Exception
{
    public ErrorViewModel Erro { get; set; } = new();
    public ValidacaoException(IEnumerable<string> erros) : base(string.Join(", ", erros))
    {
        Erro.Erros = erros.Select(e => new Error { Erro = e });
    }

    public ValidacaoException(string message) : base(message)
    {
        Erro.Erros = [new() { Erro = message }];
    }
}

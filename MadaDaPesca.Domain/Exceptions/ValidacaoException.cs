using System.Net;

namespace MadaDaPesca.Domain.Exceptions;

public class ValidacaoException : Exception
{
    public ErrorViewModel Erro { get; set; } = new();
    public HttpStatusCode? HttpStatusCode { get; set; }
    public ValidacaoException(IEnumerable<string> erros, HttpStatusCode? httpStatusCode = null) : base(string.Join(", ", erros))
    {
        Erro.Erros = erros.Select(e => new Error { Erro = e });
        HttpStatusCode = httpStatusCode;
    }

    public ValidacaoException(string message, HttpStatusCode? httpStatusCode = null) : base(message)
    {
        Erro.Erros = [new() { Erro = message }];
        HttpStatusCode = httpStatusCode;
    }
}

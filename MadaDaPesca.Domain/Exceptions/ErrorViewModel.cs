namespace MadaDaPesca.Domain.Exceptions;

public class ErrorViewModel
{
    public IEnumerable<Error> Erros { get; set; } = [];
}

public class Error
{
    public string Erro { get; set; } = string.Empty;
}

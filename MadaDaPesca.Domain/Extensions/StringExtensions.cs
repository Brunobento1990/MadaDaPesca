using MadaDaPesca.Domain.Exceptions;

namespace MadaDaPesca.Domain.Extensions;

public static class StringExtensions
{
    public static string ValidarNull(this string value, string erro)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidacaoException(erro);
        }
        return value;
    }

    public static string RemoverAcentos(this string text)
    {
        string withDiacritics = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        string withoutDiacritics = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
        for (int i = 0; i < withDiacritics.Length; i++)
        {
            text = text.Replace(withDiacritics[i].ToString(), withoutDiacritics[i].ToString());
        }
        return text;
    }

    public static string ValidarLength(this string value, int length, string erro)
    {
        if (value?.Length > length)
        {
            throw new ValidacaoException(erro);
        }
        return value ?? "";
    }

    public static string? ValidarLengthNull(this string? value, int length, string erro)
    {
        if (value?.Length > length)
        {
            throw new ValidacaoException(erro);
        }
        return value;
    }

    public static string LimparMascaraCpf(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return cpf;
        }
        return cpf
            .Replace(".", "")
            .Replace("-", "")
            .Replace("/", "")
            .Trim();
    }

    public static string LimparMascaraTelefone(this string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
        {
            return telefone;
        }
        return telefone
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace(" ", "")
            .Trim();
    }
}

using static BCrypt.Net.BCrypt;

namespace MadaDaPesca.Application.Adapters;

internal static class PasswordAdapter
{
    public static bool VerifyPassword(string confirmPassword, string password)
    {
        return Verify(confirmPassword, password);
    }

    public static string GenerateHash(string password)
    {
        return HashPassword(password, 10);
    }
}

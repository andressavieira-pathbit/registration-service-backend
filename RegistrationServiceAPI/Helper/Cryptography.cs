using System.Security.Cryptography;
using System.Text;

namespace RegistrationServiceAPI.Helper;

public static class Cryptography
{
    public static string EncryptPassword(this string password)
    {
        var hash = SHA256.Create();
        var encodedValue = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        var sb = new StringBuilder();
        foreach (var item in encodedValue)
        {
            sb.Append(item.ToString("x2"));
        }

        return sb.ToString();
    }
}

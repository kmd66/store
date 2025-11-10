using System.Security.Cryptography;
using System.Text;

namespace MizeBazi.Store.Common.Helper;

internal class HashSHA256
{
    public static string Hash(string input)
    {
        using (HashAlgorithm hashAlgorithm = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] inArray = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
using System.Text;
using System.Text.RegularExpressions;

namespace MizeBazi.Store.Common.Helper;

internal class Base64
{
    public static string Decrypt(string plainText)
    {
        byte[] bytes = Convert.FromBase64String(plainText);
        return Encoding.UTF8.GetString(bytes);
    }

    public static string Encrypt(string plainText)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(bytes);
    }

    public static string[] Decode(string s)
    {
        byte[] bytes = Convert.FromBase64String(s);
        string @string = Encoding.UTF8.GetString(bytes);
        Regex regex = new Regex("::");
        return regex.Split(@string);
    }
}
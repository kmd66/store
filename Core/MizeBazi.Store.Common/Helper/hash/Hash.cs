using System.Text;

namespace MizeBazi.Store.Common.Helper;
public static class Hash
{
    public static string GetSecurityStamp(this int validHour)
    {
        return string.Format("{0}|{1}", Guid.NewGuid().ToString("N"), DateTime.Now.AddHours(validHour).Ticks);
    }

    public static string GetDigitsFromString(this string input, int from, int to)
        => new string(input.Where(char.IsDigit).ToArray()).Substring(from, to);

    public static string GenerateOrderNumber()
        => $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

    public static string GetDigitsFromGuid()
        => Guid.NewGuid().ToString().GetDigitsFromString(0, 5);

    public static string Md5(this string plainText)
    {
        return HashMd5.Hash(plainText);
    }


    public static string Base64Encrypt(this string plainText)
    {
        return Base64.Encrypt(plainText);
    }
    public static string Base64Encrypt(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    public static string Base64Decrypt(this string plainText)
    {
        return Base64.Decrypt(plainText);
    }

    public static string HashText(this string plainText)
    {
        return HashSHA256.Hash("!<" + plainText + "]?");
    }

    public static string SHA256(this string plainText)
    {
        return HashSHA256.Hash(plainText);
    }


    public static string RsaEncrypt(this string plainText)
    {
        return Rsa.Encrypt(plainText);
    }

    public static string RsaDecrypt(this string plainText)
    {
        return Rsa.Decrypt(plainText);
    }
    public static string AesEncrypt(this string plainText, string k, string i)
    {
        return AesEncryption.Encrypt(plainText, Encoding.UTF8.GetBytes(k), Encoding.UTF8.GetBytes(i));
    }

    public static string AesDecrypt(this string plainText, string k, string i)
    {
        return AesEncryption.Decrypt(plainText, Encoding.UTF8.GetBytes(k), Encoding.UTF8.GetBytes(i));
    }

}
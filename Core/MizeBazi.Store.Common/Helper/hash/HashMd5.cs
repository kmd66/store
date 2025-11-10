using System.Security.Cryptography;
using System.Text;

namespace MizeBazi.Store.Common.Helper;

internal class HashMd5
{
    public static string Hash(string plainText)
    {
        MD5 mD = MD5.Create();
        byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(plainText));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2"));
        }

        return stringBuilder.ToString();
    }
}
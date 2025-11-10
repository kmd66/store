using MizeBazi.Store.Common.Shared;
using System.Security.Cryptography;
using System.Text;

namespace MizeBazi.Store.Common.Helper;

internal class Rsa
{
    private static DeploymentMode _deploymentMode;

    private static int maxLength = 55;

    private static RsaModel _keyModel { get; set; }

    public static void Register(string deploymentMode, RsaModel keyModel)
    {
        _deploymentMode = ((!(deploymentMode == "2")) ? DeploymentMode.Development : DeploymentMode.Prodoction);
        _keyModel = keyModel;
    }

    public static string Encrypt(string input)
    {
        string text = "";
        if (string.IsNullOrEmpty(input))
        {
            return text;
        }

        double num = Math.Ceiling((double)input.Length / (double)maxLength);
        for (int i = 0; (double)i < num; i++)
        {
            int num2 = i * maxLength;
            string strText = ((num2 + maxLength > input.Length) ? input.Substring(num2, input.Length - num2) : input.Substring(num2, maxLength));
            string text2 = _Encryption(strText);
            text = text + text2 + " ";
        }

        return text;
    }

    public static string Decrypt(string input)
    {
        string text = "";
        if (string.IsNullOrEmpty(input))
        {
            return text;
        }

        string[] array = input.Split(' ');
        string[] array2 = array;
        foreach (string text2 in array2)
        {
            string text3 = text2.Trim();
            if (string.IsNullOrEmpty(text3))
            {
                break;
            }

            text += _Decryption(text3);
        }

        return text;
    }

    public static T Deserialize<T>(T input)
    {
        if (_deploymentMode == DeploymentMode.Development)
        {
            return input;
        }

        dynamic val = input;
        if (val == null || string.IsNullOrEmpty(val.Hash))
        {
            throw new Exception("Hash model is null");
        }

        dynamic val2 = Rsa.Decrypt(val.Hash);
        dynamic val3 = System.Text.Json.JsonSerializer.Deserialize<T>(val2);
        return val3;
    }

    public static RsaModel KeyGeneration()
    {
        RsaModel rsaModel = new RsaModel();
        using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(1024))
        {
            try
            {
                rsaModel.privateKey = rSACryptoServiceProvider.ToXmlString(includePrivateParameters: true);
                rsaModel.publicKey = rSACryptoServiceProvider.ToXmlString(includePrivateParameters: false);
                using (new RSACryptoServiceProvider(1024))
                {
                    try
                    {
                        rSACryptoServiceProvider.FromXmlString(rsaModel.publicKey);
                        rsaModel.jsPublicKey = _ExportPublicKey(rSACryptoServiceProvider);
                    }
                    finally
                    {
                        rSACryptoServiceProvider.PersistKeyInCsp = false;
                    }
                }
            }
            finally
            {
                rSACryptoServiceProvider.PersistKeyInCsp = false;
            }
        }

        return rsaModel;
    }

    private static string _Encryption(string strText)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(strText);
        using RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(1024);
        try
        {
            byte[] bytes2 = Encoding.UTF8.GetBytes("ب");
            byte[] bytes3 = Encoding.UTF8.GetBytes("d");
            int length = strText.Length;
            rSACryptoServiceProvider.FromXmlString(_keyModel.publicKey.ToString());
            byte[] inArray = rSACryptoServiceProvider.Encrypt(bytes, fOAEP: false);
            return Convert.ToBase64String(inArray);
        }
        finally
        {
            rSACryptoServiceProvider.PersistKeyInCsp = false;
        }
    }

    private static string _Decryption(string strText)
    {
        using RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(1024);
        try
        {
            rSACryptoServiceProvider.FromXmlString(_keyModel.privateKey);
            byte[] rgb = Convert.FromBase64String(strText);
            byte[] bytes = rSACryptoServiceProvider.Decrypt(rgb, fOAEP: false);
            string @string = Encoding.UTF8.GetString(bytes);
            return @string.ToString();
        }
        finally
        {
            rSACryptoServiceProvider.PersistKeyInCsp = false;
        }
    }

    private static string _ExportPublicKey(RSACryptoServiceProvider csp)
    {
        RSAParameters rSAParameters = csp.ExportParameters(includePrivateParameters: false);
        using MemoryStream memoryStream = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
        binaryWriter.Write((byte)48);
        using (MemoryStream memoryStream2 = new MemoryStream())
        {
            BinaryWriter stream = new BinaryWriter(memoryStream2);
            _EncodeIntegerBigEndian(stream, new byte[1]);
            _EncodeIntegerBigEndian(stream, rSAParameters.Modulus);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            _EncodeIntegerBigEndian(stream, rSAParameters.Exponent);
            int num = (int)memoryStream2.Length;
            _EncodeLength(binaryWriter, num);
            binaryWriter.Write(memoryStream2.GetBuffer(), 0, num);
        }

        char[] array = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length).ToCharArray();
        StringBuilder stringBuilder = new StringBuilder();
        string text = "-----BEGIN PUBLIC KEY-----";
        stringBuilder.AppendLine("-----BEGIN PUBLIC KEY-----");
        for (int i = 0; i < array.Length; i += 64)
        {
            int num2 = Math.Min(64, array.Length - i);
            for (int j = 0; j < num2; j++)
            {
                stringBuilder.Append(array[i + j]);
                text += array[i + j];
            }

            stringBuilder.AppendLine();
        }

        text += "-----END PUBLIC KEY-----";
        stringBuilder.AppendLine("-----END PUBLIC KEY-----");
        return stringBuilder.ToString();
    }

    private static void _EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
    {
        stream.Write((byte)2);
        int num = 0;
        for (int i = 0; i < value.Length && value[i] == 0; i++)
        {
            num++;
        }

        if (value.Length - num == 0)
        {
            _EncodeLength(stream, 1);
            stream.Write((byte)0);
            return;
        }

        if (forceUnsigned && value[num] > 127)
        {
            _EncodeLength(stream, value.Length - num + 1);
            stream.Write((byte)0);
        }
        else
        {
            _EncodeLength(stream, value.Length - num);
        }

        for (int j = num; j < value.Length; j++)
        {
            stream.Write(value[j]);
        }
    }

    private static void _EncodeLength(BinaryWriter stream, int length)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
        }

        if (length < 128)
        {
            stream.Write((byte)length);
            return;
        }

        int num = length;
        int num2 = 0;
        while (num > 0)
        {
            num >>= 8;
            num2++;
        }

        stream.Write((byte)((uint)num2 | 0x80u));
        for (int num3 = num2 - 1; num3 >= 0; num3--)
        {
            stream.Write((byte)((uint)(length >> 8 * num3) & 0xFFu));
        }
    }
}
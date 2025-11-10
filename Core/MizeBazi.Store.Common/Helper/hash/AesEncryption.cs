using System.Security.Cryptography;

namespace MizeBazi.Store.Common.Helper;

internal class AesEncryption
{
    public static string Encrypt(string plainText, byte[] k, byte[] i)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = k;
            aesAlg.IV = i;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string cipherText, byte[] k, byte[] i)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = k;
            aesAlg.IV = i;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    /*
nodejs

const crypto = require('crypto');

const algorithm = 'aes-256-cbc';
const key = Buffer.from('12345678901234567890123456789012', 'utf8'); // 32 bytes (256 bits)
const iv = Buffer.from('1234567890123456', 'utf8'); // 16 bytes (128 bits)

function encrypt(text) {
    let cipher = crypto.createCipheriv(algorithm, key, iv);
    let encrypted = cipher.update(text, 'utf8', 'base64');
    encrypted += cipher.final('base64');
    return encrypted;
}

function decrypt(encryptedText) {
    let decipher = crypto.createDecipheriv(algorithm, key, iv);
    let decrypted = decipher.update(encryptedText, 'base64', 'utf8');
    decrypted += decipher.final('utf8');
    return decrypted;
}

// مثال استفاده
const original = "Hello, World!";
const encrypted = encrypt(original);
const decrypted = decrypt(encrypted);

console.log(`Original: ${original}`);
console.log(`Encrypted: ${encrypted}`);
console.log(`Decrypted: ${decrypted}`);

     */
}
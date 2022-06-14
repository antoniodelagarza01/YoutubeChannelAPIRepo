using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptorDecryptor
{
    public static class EncryptorDecryptor
    {
       
        public static string Encrypt(string input, string iv, string key)
        {

            byte[] textBytes = UnicodeEncoding.Unicode.GetBytes(input);
            AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
            provider.BlockSize = 128;
            provider.KeySize = 256;
            provider.IV = UnicodeEncoding.ASCII.GetBytes(iv);
            provider.Key = UnicodeEncoding.ASCII.GetBytes(key);
            provider.Padding = PaddingMode.PKCS7;
            provider.Mode = CipherMode.CBC;
            ICryptoTransform cryptoTransform = provider.CreateEncryptor(provider.Key, provider.IV);
            byte[] encrypt = cryptoTransform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            cryptoTransform.Dispose();
            return Convert.ToBase64String(encrypt);
        }
        public static string Decrypt(string input, string iv, string key)
        {
            byte[] textbytes = Convert.FromBase64String(input);
            AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
            endec.BlockSize = 128;
            endec.KeySize = 256;
            endec.IV = UnicodeEncoding.ASCII.GetBytes(iv);
            endec.Key = UnicodeEncoding.ASCII.GetBytes(key);
            endec.Padding = PaddingMode.PKCS7;
            endec.Mode = CipherMode.CBC;
            ICryptoTransform icrypt = endec.CreateDecryptor(endec.Key, endec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();
            return UnicodeEncoding.Unicode.GetString(enc);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace encryptionFilesAES
{
    class ServiceAES
    {
        public AesCryptoServiceProvider AesCsp;

        public ServiceAES(CipherMode mode, string password, bool decryption)
        {
            if (decryption)
            {
                AesCsp = new AesCryptoServiceProvider
                {
                    BlockSize = 128,
                    KeySize = 256,
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                AesCsp.Key = GetHashSha256(password);
            }
            else
            {
                AesCsp = new AesCryptoServiceProvider
                {
                    BlockSize = 128,
                    KeySize = 256,
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                AesCsp.GenerateIV();
                AesCsp.Key = GetHashSha256(password);
            }
        }

        public string Encrypt(string text)
        {
            var encryptor = AesCsp.CreateEncryptor();

            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(text);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string encryptedText, byte[] IV)
        {
            AesCsp.IV = IV;
            var decryptor = AesCsp.CreateDecryptor();

            string text = null;
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.rijndael.Padding = PaddingMode.None;

                        text = srDecrypt.ReadToEnd();
                    }
                }
            }

            return text;
        }

        public static byte[] GetHashSha256(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            return hash;
        }

        public string GetIV()
        {
            return Convert.ToBase64String(AesCsp.IV);
        }
    }
}

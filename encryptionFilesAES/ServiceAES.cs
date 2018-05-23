using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace encryptionFilesAES
{
    class ServiceAES
    {
        public AesCryptoServiceProvider AesCsp;

        public ServiceAES(CipherMode mode, string password)
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

        public string Encrypt(string text)
        {
            var transform = AesCsp.CreateEncryptor();
            var encryptedBytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(text),
                                                            0, text.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        public byte[] GetHashSha256(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            //var hashString = string.Empty;
            //foreach (var element in hash)
            //{
            //    hashString += String.Format("{0:X2}", element);
            //}

            return hash;
        }
    }
}

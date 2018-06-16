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

        public ServiceAES(CipherMode mode, string password)
        {
            AesCsp = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 192,
                Mode = mode,
                Padding = PaddingMode.PKCS7
            };
            AesCsp.GenerateIV();
            AesCsp.Key = GetHashSha256(password);
        }

        public string Encrypt(string text)
        {
            var transform = AesCsp.CreateEncryptor();
            var encryptedBytes = transform.TransformFinalBlock(Encoding.UTF8.GetBytes(text),
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

        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        public void EncryptFile(string inputFile)
        {
            byte[] salt = GenerateRandomSalt();
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

        }
    }
}

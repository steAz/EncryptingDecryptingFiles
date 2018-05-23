using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace encryptionFilesAES
{
    class ServiceRSA
    {
        public RSACryptoServiceProvider RSAcsp;
        private RSAParameters paramsPrivKey;
        private RSAParameters paramsPubKey;

        public RSAParameters ParamsPubKey { get => paramsPubKey; set => paramsPubKey = value; }
        public RSAParameters ParamsPrivKey { get => paramsPrivKey; set => paramsPrivKey = value; }


        public ServiceRSA()
        {
            RSAcsp = new RSACryptoServiceProvider(2048);
            ParamsPrivKey = RSAcsp.ExportParameters(true);
            ParamsPubKey = RSAcsp.ExportParameters(false);
        }

        public ServiceRSA(string pubKey)
        {
            RSAcsp = new RSACryptoServiceProvider(2048);
            ParamsPubKey = KeyToParamsKey(pubKey);
        }

        /// <summary>
        /// //converting the key into a string representation
        /// </summary>
        /// <returns> string representation of Key </returns>
        public string ParamsKeyToString(RSAParameters paramsKey)
        {
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, paramsKey);
            return sw.ToString();
        }

        private RSAParameters KeyToParamsKey(string key)
        {
            var sr = new System.IO.StringReader(key);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            return (RSAParameters)xs.Deserialize(sr);
        }

        public string EncryptSessionKey(string text)
        {
            RSAcsp.ImportParameters(ParamsPubKey); // the key which will be used to encryption is PubKey set earlier in constructor with an argument
            var bytesText = Encoding.UTF8.GetBytes(text);
            var bytesCypherText = RSAcsp.Encrypt(bytesText, false);
            return Convert.ToBase64String(bytesCypherText);
        }

    }
}

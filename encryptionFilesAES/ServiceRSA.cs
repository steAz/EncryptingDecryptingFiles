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


        /// <summary>
        /// //converting the public key into a string representation
        /// </summary>
        /// <returns> string representation of PubKey </returns>
        public string KeyToString(RSAParameters paramsKey)
        {
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, paramsKey);
            return sw.ToString();
        }

    }
}

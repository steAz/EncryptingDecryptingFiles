using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace encryptionFilesAES
{
    class User
    {
        public string name;
        public string encryptedSessionKey;

        public User(string name, string encryptedSessionKey)
        {
            this.name = name;
            this.encryptedSessionKey = encryptedSessionKey;
        }
    }
}

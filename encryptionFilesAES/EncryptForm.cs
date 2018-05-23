using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;

namespace encryptionFilesAES
{
    public partial class EncryptForm : Form
    {
        struct User
        {
            public string name;
            public string encryptedSessionKey;

            public User(string name, string encryptedSessionKey)
            {
                this.name = name;
                this.encryptedSessionKey = encryptedSessionKey;
            }
        }

        public EncryptForm()
        {
            InitializeComponent();
            string[] usernames = Directory.GetFiles(@"..\..\users\publicKeys", "*.txt")
                                         .Select(Path.GetFileNameWithoutExtension)
                                         .ToArray();
            foreach (var username in usernames)
                this.approvedUsersLB.Items.Add(username);
            encryptMessageLabel.Hide();
        }

        public string GetSessionKey(int size)
        {
            string legalCharacters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=[],.;!@#$%^&*()|";
            StringBuilder builder = new StringBuilder();
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = legalCharacters[random.Next(0, legalCharacters.Length)];
                builder.Append(ch);
            }

            return builder.ToString();
        }


        private void Encrypt_Click(object sender, EventArgs e)
        {
            List<User> users = new List<User>();

            foreach (var username in approvedUsersLB.CheckedItems)
            {
                string pubKey;
                using (StreamReader sr = new StreamReader(@"..\..\users\publicKeys\" + username + ".txt"))
                {
                    pubKey = sr.ReadToEnd();
                }
                var serviceRSA = new ServiceRSA(pubKey);
                var sessionKey = GetSessionKey(245); // 245, because it's the maximum number of bytes that can be encrypted by asymetric RSA
                var encryptedSessionKey = serviceRSA.EncryptSessionKey(sessionKey); // Encrypting session key of the user by public key of the same user
                users.Add(new User(username.ToString(), encryptedSessionKey));
            }

            CipherMode mode = CipherMode.ECB;
            var cipherMode = encryptionModeCB.SelectedItem.ToString();
            if (cipherMode == "ECB")
                mode = CipherMode.ECB;
            else if (cipherMode == "CBC")
                mode = CipherMode.CBC;
            else if (cipherMode == "OFB")
                mode = CipherMode.OFB;
            else if (cipherMode == "CFB")
                mode = CipherMode.CFB;

            var serviceAES = new ServiceAES(mode, ".");

            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement element1 = doc.CreateElement(string.Empty, "EncryptedFileHeader", string.Empty);
            doc.AppendChild(element1);


            XmlElement element2 = doc.CreateElement(string.Empty, "Algorithm", string.Empty);
            XmlText text1 = doc.CreateTextNode("AES");
            element2.AppendChild(text1);
            element1.AppendChild(element2);

            element2 = doc.CreateElement(string.Empty, "KeySize", string.Empty);
            text1 = doc.CreateTextNode(serviceAES.AesCsp.KeySize.ToString());
            element2.AppendChild(text1);
            element1.AppendChild(element2);

            element2 = doc.CreateElement(string.Empty, "BlockSize", string.Empty);
            text1 = doc.CreateTextNode(serviceAES.AesCsp.BlockSize.ToString());
            element2.AppendChild(text1);
            element1.AppendChild(element2);

            element2 = doc.CreateElement(string.Empty, "CipherMode", string.Empty);
            text1 = doc.CreateTextNode(cipherMode);
            element2.AppendChild(text1);
            element1.AppendChild(element2);

            element2 = doc.CreateElement(string.Empty, "IV", string.Empty);
            string IV = Encoding.Unicode.GetString(serviceAES.AesCsp.IV);
            text1 = doc.CreateTextNode(IV);
            element2.AppendChild(text1);
            element1.AppendChild(element2);

            element2 = doc.CreateElement(string.Empty, "ApprovedUsers", string.Empty);
            foreach(var user in users)
            {
                var element3 = doc.CreateElement(string.Empty, "User", string.Empty);
                element2.AppendChild(element3);
                var element4 = doc.CreateElement(string.Empty, "Email", string.Empty);
                text1 = doc.CreateTextNode(user.name);
                element4.AppendChild(text1);
                element3.AppendChild(element4);

                element4 = doc.CreateElement(string.Empty, "SessionKey", string.Empty);
                text1 = doc.CreateTextNode(user.encryptedSessionKey);
                element4.AppendChild(text1);
                element3.AppendChild(element4);
            }
            element1.AppendChild(element2);


            var dirToSave = fileTB.Text.Substring(0, fileTB.Text.LastIndexOf("\\") + 1);
            doc.Save(dirToSave + outputFilenameTB.Text);

        }


        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileTB.Text = openFileDialog.FileName;
            }
        }
    }
}

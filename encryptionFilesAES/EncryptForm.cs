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
using System.Security.Cryptography;
using System.Xml.Linq;

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
            var users = new List<User>();

            var keySize = 192;
            var blockSize = 128;
            var sessionKey = GetSessionKey(keySize/8); // length of key must be keySize/8
            foreach (var username in approvedUsersLB.CheckedItems)
            {
                string pubKey;
                using (StreamReader sr = new StreamReader(@"..\..\users\publicKeys\" + username + ".txt"))
                {
                    pubKey = sr.ReadToEnd();
                }
                var serviceRSA = new ServiceRSA(pubKey);
                var encryptedSessionKey = serviceRSA.EncryptSessionKey(sessionKey); // Encrypting session key of the user by public key of the same user
                users.Add(new User(username.ToString(), encryptedSessionKey));
            }


            if (encryptionModeCB.SelectedItem == null)
            {
                MessageBox.Show("You must choose encryption mode");
                return;
            }

            CipherMode mode = 0;
            var cipherMode = encryptionModeCB.SelectedItem.ToString();
            if (cipherMode == "ECB")
                mode = CipherMode.ECB;
            else if (cipherMode == "CBC")
                mode = CipherMode.CBC;
            else if (cipherMode == "OFB")
            {
                MessageBox.Show("OFB isnt supported in .NET");
                return;
            }
            else if (cipherMode == "CFB")
                mode = CipherMode.CFB;

            var headers = new XElement("EncryptedFileHeader");
            headers.Add(new XElement("Algorithm", "AES"));
            headers.Add(new XElement("KeySize", "196")); 
            headers.Add(new XElement("BlockSize", "128"));
            headers.Add(new XElement("CipherMode", cipherMode));
            headers.Add(new XElement("IV", "XD"));


            var approvedUsers = new XElement("ApprovedUsers");
            foreach (var user in users)
            {
                approvedUsers.Add(new XElement("User",
                                    new XElement("Email", user.name),
                                    new XElement("SessionKey", user.encryptedSessionKey)));
            }

            headers.Add(new XElement(approvedUsers));

            var xmlFile = new XDocument(headers);

            var dirToSave = fileTB.Text.Substring(0, fileTB.Text.LastIndexOf("\\") + 1);
            var outputFileName = dirToSave + outputFilenameTB.Text;
            xmlFile.Save(outputFileName);

            using (var swEncrypt = File.AppendText(outputFileName))
            {

                //Write all data to the stream.
                var sessionKeyInBytes = Encoding.ASCII.GetBytes(sessionKey);
                    swEncrypt.Write(
                        Convert.ToBase64String(
                            ServiceRijndaelAES.EncryptStringToBytes(
                                File.ReadAllText(
                                    fileTB.Text), sessionKeyInBytes, mode, keySize, blockSize)));
            }
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

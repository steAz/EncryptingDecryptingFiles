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

namespace encryptionFilesAES
{
    public partial class EncryptForm : Form
    {
        struct User
        {
            string name;
            string sessionKey;

            public User(string name, string sessionKey)
            {
                this.name = name;
                this.sessionKey = sessionKey;
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
            var sessionKey = GetSessionKey(2048);
            MessageBox.Show(sessionKey);
            List<User> users = new List<User>();

            foreach (var username in approvedUsersLB.CheckedItems)
                users.Add(new User(username.ToString(), GetSessionKey(2048)));
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

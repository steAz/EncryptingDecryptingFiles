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
        private BackgroundWorker backgroundWorker1;


        public EncryptForm()
        {
            InitializeComponent();
            string[] usernames = Directory.GetFiles(@"..\..\users\publicKeys", "*.txt")
                                         .Select(Path.GetFileNameWithoutExtension)
                                         .ToArray();
            foreach (var username in usernames)
                this.approvedUsersLB.Items.Add(username);
            encryptMessageLabel.Hide();
            encryptPB.Hide();

            this.backgroundWorker1 = new BackgroundWorker();
            InitializeBackgroundWorker();
        }


        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
               new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
              new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
        }


        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            encryptPB.Value = e.ProgressPercentage;
        }


        private void BackgroundWorker1_DoWork(object sender,
           DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.WorkerReportsProgress = true;
            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    //System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(i * 10);
                }
            }
        }


        private void BackgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            encryptPB.Value = 100;
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
            encryptMessageLabel.Show();  
            this.encryptMessageLabel.ForeColor = Color.Orange;
            this.encryptMessageLabel.Text = "Encryption in progress";

            if (encryptionModeCB.SelectedItem == null)
            {
                this.encryptMessageLabel.ForeColor = Color.Red;
                this.encryptMessageLabel.Text = "Encryption failed";
                encryptPB.Hide();
                MessageBox.Show("No encryption mode was chosen");
                return;
            }
            if (outputFilenameTB.Text.Equals(string.Empty))
            {
                this.encryptMessageLabel.ForeColor = Color.Red;
                this.encryptMessageLabel.Text = "Encryption failed";
                encryptPB.Hide();
                MessageBox.Show("No output filename was chosen");
                return;
            }
            if (fileTB.Text.Equals(string.Empty))
            {
                this.encryptMessageLabel.ForeColor = Color.Red;
                this.encryptMessageLabel.Text = "Encryption failed";
                encryptPB.Hide();
                MessageBox.Show("No input file was chosen");
                return;
            }
            if (approvedUsersLB.CheckedItems.Count <= 0)
            {
                this.encryptMessageLabel.ForeColor = Color.Red;
                this.encryptMessageLabel.Text = "Encryption failed";
                encryptPB.Hide();
                MessageBox.Show("No receiver/receivers was/were chosen");
                return;
            }

            var users = new List<User>();

            byte[] fileIV;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.GenerateIV();
                fileIV = rijAlg.IV;
            }

            var keySize = 192;
            var blockSize = 128;
            var sessionKey = GetSessionKey(keySize/8); // length of key must be keySize/8
            foreach (var username in approvedUsersLB.CheckedItems)
            {
                string pubKey, privKey;
                using (StreamReader sr = new StreamReader(@"..\..\users\publicKeys\" + username + ".txt"))
                {
                    pubKey = sr.ReadToEnd();
                }
                using (StreamReader sr = new StreamReader(@"..\..\users\privateKeys\" + username + ".txt"))
                {
                    privKey = sr.ReadToEnd();
                }
                var serviceRSA = new ServiceRSA(pubKey);
                var encryptedSessionKey = serviceRSA.EncryptSessionKey(sessionKey); // Encrypting session key of the user by public key of the same user
                users.Add(new User(username.ToString(), encryptedSessionKey));
            }
    
            CipherMode mode = 0;
            var cipherMode = encryptionModeCB.SelectedItem.ToString();
            if (cipherMode == "ECB")
                mode = CipherMode.ECB;
            else if (cipherMode == "CBC")
                mode = CipherMode.CBC;
            else if (cipherMode == "OFB")
            {
                this.encryptMessageLabel.ForeColor = Color.Red;
                this.encryptMessageLabel.Text = "Encryption failed";
                MessageBox.Show("OFB isnt supported in .NET");
                encryptPB.Hide();
                return;
            }
            else if (cipherMode == "CFB")
                mode = CipherMode.CFB;

            var headers = new XElement("EncryptedFileHeader");
            headers.Add(new XElement("Algorithm", "AES"));
            headers.Add(new XElement("KeySize", keySize)); 
            headers.Add(new XElement("BlockSize", blockSize));
            headers.Add(new XElement("CipherMode", cipherMode));
            headers.Add(new XElement("IV", Convert.ToBase64String(fileIV)));
            var extensionOfFile = Path.GetExtension(fileTB.Text);
            headers.Add(new XElement("Extension", extensionOfFile));
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

            encryptPB.Show();
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }

            using (var swEncrypt = File.AppendText(outputFileName))
            {
                //Write all data to the stream.
                var sessionKeyInBytes = Encoding.ASCII.GetBytes(sessionKey);
                    swEncrypt.Write(
                        Convert.ToBase64String(
                            ServiceRijndaelAES.EncryptStringToBytes(this, 
                                File.ReadAllText(
                                    fileTB.Text), sessionKeyInBytes, mode, fileIV)));
            }

            this.encryptMessageLabel.ForeColor = Color.Green;
            this.encryptMessageLabel.Text = "Encryption suceeded";
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

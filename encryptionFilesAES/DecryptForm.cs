using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace encryptionFilesAES
{
    public partial class DecryptForm : Form
    {
        private BackgroundWorker backgroundWorker1;


        public DecryptForm()
        {
            InitializeComponent();
            string[] usernames = Directory.GetFiles(@"..\..\users\publicKeys", "*.txt")
                                         .Select(Path.GetFileNameWithoutExtension)
                                         .ToArray();
            foreach (var username in usernames)
                this.approvedUsersCB.Items.Add(username);

            decryptMessageLabel.Hide();
            this.progressBar.Hide();

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
            progressBar.Value = e.ProgressPercentage;
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
            progressBar.Value = 100;
        }


        private void Decrypt_Click(object sender, EventArgs e)
        {
           
            decryptMessageLabel.Show();      
            this.decryptMessageLabel.ForeColor = Color.Orange;
            this.decryptMessageLabel.Text = "Decryption in progress";

            if (outputFilenameTB.Text.Equals(string.Empty))
            {
                this.decryptMessageLabel.ForeColor = Color.Red;
                this.decryptMessageLabel.Text = "Decryption failed";
                progressBar.Hide();
                MessageBox.Show("No output filename was chosen");
                return;
            }
            if (fileTB.Text.Equals(string.Empty))
            {
                this.decryptMessageLabel.ForeColor = Color.Red;
                this.decryptMessageLabel.Text = "Decryption failed";
                progressBar.Hide();
                MessageBox.Show("No input file was chosen");
                return;
            }
            if (approvedUsersCB.Text.Equals(string.Empty))
            {
                this.decryptMessageLabel.ForeColor = Color.Red;
                this.decryptMessageLabel.Text = "Decryption failed";
                progressBar.Hide();
                MessageBox.Show("No user was chosen");
                return;
            }
            if (userPassTB.Text.Equals(string.Empty))
            {
                this.decryptMessageLabel.ForeColor = Color.Red;
                this.decryptMessageLabel.Text = "Decryption failed";
                progressBar.Hide();
                MessageBox.Show("Incorrect password");
                return;
            }

            var encryptedFileContent = File.ReadAllText(fileTB.Text);
            var XMLStringMetadata = encryptedFileContent.Split(new[] { "</EncryptedFileHeader>" }, StringSplitOptions.None)[0] + "</EncryptedFileHeader>";
            var encyptedData = encryptedFileContent.Split(new[] { "</EncryptedFileHeader>" }, StringSplitOptions.None)[1];
            var doc = new XmlDocument();
            doc.LoadXml(XMLStringMetadata);
            XmlNodeList approvedUserNodes = doc.SelectNodes("/EncryptedFileHeader/ApprovedUsers/User");
            var decryptionMode = doc.SelectSingleNode("/EncryptedFileHeader/CipherMode").InnerText;
            var fileIV = doc.SelectSingleNode("/EncryptedFileHeader/IV").InnerText;

            CipherMode mode = 0;
            var cipherMode = decryptionMode;
            if (cipherMode == "ECB")
                mode = CipherMode.ECB;
            else if (cipherMode == "CBC")
                mode = CipherMode.CBC;
            else if (cipherMode == "CFB")
                mode = CipherMode.CFB;

            this.decryptMessageLabel.ForeColor = Color.Red;
            this.decryptMessageLabel.Text = "Decryption failed"; //this message is needed, when the user choose receiver that is not in the header of the file

            foreach (XmlNode node in approvedUserNodes)
            {
                if (approvedUsersCB.Text == node.SelectSingleNode("Email").InnerText)
                {
                    progressBar.Show();
                    if (backgroundWorker1.IsBusy != true)
                    {
                        // Start the asynchronous operation.
                        backgroundWorker1.RunWorkerAsync();
                    }

                    var encryptedSessionKey = node.SelectSingleNode("SessionKey").InnerText;
                    string encryptedPrivKey, userIV;
                    using (StreamReader sr = new StreamReader(@"..\..\users\privateKeys\" + approvedUsersCB.Text + ".txt"))
                    {
                        encryptedPrivKey = sr.ReadToEnd();
                    }
                    using (StreamReader sr = new StreamReader(@"..\..\users\vectorsIV\" + approvedUsersCB.Text + ".txt"))
                    {
                        userIV = sr.ReadToEnd();
                    }

                    var serviceAESDecr = new ServiceAES(CipherMode.ECB, userPassTB.Text, true);
                    var privKey = serviceAESDecr.Decrypt(encryptedPrivKey, Convert.FromBase64String(userIV));
                    var serviceRSADecr = new ServiceRSA(privKey, true);
                    var sessionKey = serviceRSADecr.DecryptSessionKey(encryptedSessionKey);

                    var extensionOfFile = Path.GetExtension(doc.SelectSingleNode("/EncryptedFileHeader/Extension").InnerText);
                    var dirToSave = fileTB.Text.Substring(0, fileTB.Text.LastIndexOf("\\") + 1);
                    var outputFileName = dirToSave + outputFilenameTB.Text + extensionOfFile;

                    File.WriteAllText(Path.GetFullPath(outputFileName), // START DECRYPTING
                                        ServiceRijndaelAES.DecryptStringFromBytes(
                                            Convert.FromBase64String(encyptedData),
                                                Convert.FromBase64String(sessionKey),
                                                    Convert.FromBase64String(fileIV), mode));

                    this.decryptMessageLabel.ForeColor = Color.Green;
                    this.decryptMessageLabel.Text = "Decryption suceeded";
                }    
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

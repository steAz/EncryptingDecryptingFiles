using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace encryptionFilesAES
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            Form encryptForm = new EncryptForm();
            encryptForm.Show();
            encryptForm.Location = this.Location;
            encryptForm.Left += 600;
            encryptForm.Top += 200;
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            Form addUserForm = new AddUserForm();
            addUserForm.Show();
            addUserForm.Location = this.Location;
            addUserForm.Left += 500;
            addUserForm.Top += 100;

        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            Form decryptForm = new DecryptForm();
            decryptForm.Show();
            decryptForm.Location = this.Location ;
            decryptForm.Left += 700;
            decryptForm.Top += 300;
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.Show();
            aboutForm.Location = this.Location;
            aboutForm.Left += 800;
            aboutForm.Top += 400;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace encryptionFilesAES
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
            this.errorMessageLabel.Hide();
        }

        

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }

        private void SignUp_Click(object sender, System.EventArgs e)
        {
            errorMessageLabel.Show();
            errorMessageLabel.ForeColor = Color.Red;

            if (!AreDataWrong())
            {
                AddUser();
                errorMessageLabel.ForeColor = Color.Green;
                errorMessageLabel.Text = "Good";
            }
        }

        private bool AreDataWrong()
        {
            bool dataAreWrong = false;
            if (usernameTB.Text == string.Empty || userPassTB.Text == string.Empty || userRepeatPassTB.Text == string.Empty)
            {
                dataAreWrong = true;
                errorMessageLabel.Text = "Fields are not filled";
            }

            string[] lines = File.ReadAllLines(@"..\..\users\publicKeys\data.txt");
            foreach (var line in lines)
            {
                if (!line.Equals(string.Empty))
                {
                    var len = line.IndexOf("<");
                    if (usernameTB.Text.Equals(line.Substring(0, len))) dataAreWrong = true;
                    errorMessageLabel.Text = "There is a user with such name in the database";
                }
            }


            if (!dataAreWrong && (userPassTB.Text != userRepeatPassTB.Text))
            {
                dataAreWrong = true;
                errorMessageLabel.Text = "Passwords need to be the same";
            }
            else if (!dataAreWrong 
                        && (!Regex.IsMatch(userPassTB.Text, "[a-zA-Z]") || !Regex.IsMatch(userPassTB.Text, "[0-9]") || userPassTB.Text.Length < 8))
            {
                dataAreWrong = true;
                errorMessageLabel.Text = "Password needs to have minimum: 8 characters, one digit, one letter and one special character";
            }

            if (!dataAreWrong)
            {
                string specialCharacters = "_-.,/\\:;'?*{}()[]~`!@#$%^+=";
                bool userPassHasSpecialChar = false;
                foreach (char sign in specialCharacters)
                {
                    if (userPassTB.Text.Contains(sign))
                    {
                        userPassHasSpecialChar = true;
                        break;
                    }
                }

                if (!userPassHasSpecialChar)
                {
                    dataAreWrong = true;
                    errorMessageLabel.Text = "Password needs to have minimum: one digit, one letter and one special character";
                }
            }

            return dataAreWrong;
        }

        private void CreateKeys()
        {



        }

        private void AddUser()
        {
            var serviceRSA = new ServiceRSA();
            var serviceAES = new ServiceAES(CipherMode.ECB, userPassTB.Text);
            var encryptedPrivKey = serviceAES.Encrypt(serviceRSA.KeyToString(serviceRSA.ParamsPrivKey));
            var publicKey = serviceRSA.KeyToString(serviceRSA.ParamsPubKey);

            using (StreamWriter file =
                new StreamWriter(@"..\..\users\privateKeys\data.txt", true))
            {
                file.WriteLine(usernameTB.Text + "<*>" + encryptedPrivKey);
            }
            using (StreamWriter file =
                new StreamWriter(@"..\..\users\publicKeys\data.txt", true))
            {
                file.WriteLine(usernameTB.Text + "<*>" + publicKey);
            }
        }

    }
}

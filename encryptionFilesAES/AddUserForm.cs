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

            if (!dataAreWrong)
            {
                string[] usernames = Directory.GetFiles(@"..\..\users\publicKeys", "*.txt")
                                         .Select(Path.GetFileNameWithoutExtension)
                                         .ToArray();
                foreach (string username in usernames)
                {
                    if (usernameTB.Text.Equals(username))
                    {
                        dataAreWrong = true;
                        errorMessageLabel.Text = "There is a user with such name in the database";
                        break;
                    }
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


        private void AddUser()
        {
            var serviceRSA = new ServiceRSA();
            var serviceAES = new ServiceAES(CipherMode.ECB, userPassTB.Text, false);
            var IV = serviceAES.GetIV();
            var encryptedPrivKey = serviceAES.Encrypt(serviceRSA.ParamsKeyToString(serviceRSA.ParamsPrivKey));
            var publicKey = serviceRSA.ParamsKeyToString(serviceRSA.ParamsPubKey);

            using (StreamWriter file =
                new StreamWriter(@"..\..\users\privateKeys\" + usernameTB.Text + ".txt", true))
            {
                file.Write(encryptedPrivKey);
            }
            using (StreamWriter file =
                new StreamWriter(@"..\..\users\publicKeys\" + usernameTB.Text + ".txt", true))
            {
                file.Write(publicKey);
            }
            using (StreamWriter file =
                new StreamWriter(@"..\..\users\vectorsIV\" + usernameTB.Text + ".txt", true))
            {
                file.Write(IV);
            }
        }
    }
}

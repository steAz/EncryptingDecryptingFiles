using System;

namespace encryptionFilesAES
{
    partial class AddUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.userPassLabel = new System.Windows.Forms.Label();
            this.userRepeatPassLabel = new System.Windows.Forms.Label();
            this.errorMessageLabel = new System.Windows.Forms.Label();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.userPassTB = new System.Windows.Forms.TextBox();
            this.userRepeatPassTB = new System.Windows.Forms.TextBox();
            this.signUpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(47, 25);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            // 
            // userPassLabel
            // 
            this.userPassLabel.AutoSize = true;
            this.userPassLabel.Location = new System.Drawing.Point(49, 61);
            this.userPassLabel.Name = "userPassLabel";
            this.userPassLabel.Size = new System.Drawing.Size(53, 13);
            this.userPassLabel.TabIndex = 1;
            this.userPassLabel.Text = "Password";
            // 
            // userRepeatPassLabel
            // 
            this.userRepeatPassLabel.AutoSize = true;
            this.userRepeatPassLabel.Location = new System.Drawing.Point(12, 96);
            this.userRepeatPassLabel.Name = "userRepeatPassLabel";
            this.userRepeatPassLabel.Size = new System.Drawing.Size(90, 13);
            this.userRepeatPassLabel.TabIndex = 2;
            this.userRepeatPassLabel.Text = "Repeat password";
            // 
            // errorMessageLabel
            // 
            this.errorMessageLabel.AutoSize = true;
            this.errorMessageLabel.Location = new System.Drawing.Point(28, 173);
            this.errorMessageLabel.Name = "errorMessageLabel";
            this.errorMessageLabel.Size = new System.Drawing.Size(158, 13);
            this.errorMessageLabel.TabIndex = 3;
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(128, 22);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(100, 20);
            this.usernameTB.TabIndex = 4;
            // 
            // userPassTB
            // 
            this.userPassTB.Location = new System.Drawing.Point(128, 58);
            this.userPassTB.Name = "userPassTB";
            this.userPassTB.PasswordChar = '*';
            this.userPassTB.Size = new System.Drawing.Size(100, 20);
            this.userPassTB.TabIndex = 5;
            // 
            // userRepeatPassTB
            // 
            this.userRepeatPassTB.Location = new System.Drawing.Point(128, 93);
            this.userRepeatPassTB.Name = "userRepeatPassTB";
            this.userRepeatPassTB.PasswordChar = '*';
            this.userRepeatPassTB.Size = new System.Drawing.Size(100, 20);
            this.userRepeatPassTB.TabIndex = 6;
            // 
            // signUpButton
            // 
            this.signUpButton.Location = new System.Drawing.Point(90, 131);
            this.signUpButton.Name = "signUpButton";
            this.signUpButton.Size = new System.Drawing.Size(75, 23);
            this.signUpButton.TabIndex = 7;
            this.signUpButton.Text = "Sign up";
            this.signUpButton.UseVisualStyleBackColor = true;
            this.signUpButton.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 227);
            this.Controls.Add(this.signUpButton);
            this.Controls.Add(this.userRepeatPassTB);
            this.Controls.Add(this.userPassTB);
            this.Controls.Add(this.usernameTB);
            this.Controls.Add(this.errorMessageLabel);
            this.Controls.Add(this.userRepeatPassLabel);
            this.Controls.Add(this.userPassLabel);
            this.Controls.Add(this.usernameLabel);
            this.Name = "AddUserForm";
            this.Text = "Adding user";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label userPassLabel;
        private System.Windows.Forms.Label userRepeatPassLabel;
        private System.Windows.Forms.Label errorMessageLabel;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.TextBox userPassTB;
        private System.Windows.Forms.TextBox userRepeatPassTB;
        private System.Windows.Forms.Button signUpButton;
    }
}
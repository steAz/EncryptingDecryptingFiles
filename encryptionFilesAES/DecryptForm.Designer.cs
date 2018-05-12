namespace encryptionFilesAES
{
    partial class DecryptForm
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
            this.approvedUsersLabel = new System.Windows.Forms.Label();
            this.approvedUsersCB = new System.Windows.Forms.ComboBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fileLabel = new System.Windows.Forms.Label();
            this.outputFilenameLabel = new System.Windows.Forms.Label();
            this.outputFilenameTB = new System.Windows.Forms.TextBox();
            this.decryptButton = new System.Windows.Forms.Button();
            this.decryptMessageLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // approvedUsersLabel
            // 
            this.approvedUsersLabel.AutoSize = true;
            this.approvedUsersLabel.Location = new System.Drawing.Point(253, 55);
            this.approvedUsersLabel.Name = "approvedUsersLabel";
            this.approvedUsersLabel.Size = new System.Drawing.Size(34, 13);
            this.approvedUsersLabel.TabIndex = 0;
            this.approvedUsersLabel.Text = "Users";
            // 
            // approvedUsersCB
            // 
            this.approvedUsersCB.Cursor = System.Windows.Forms.Cursors.Default;
            this.approvedUsersCB.FormattingEnabled = true;
            this.approvedUsersCB.Items.AddRange(new object[] {
            "Konstanty",
            "Oskar"});
            this.approvedUsersCB.Location = new System.Drawing.Point(256, 80);
            this.approvedUsersCB.Name = "approvedUsersCB";
            this.approvedUsersCB.Size = new System.Drawing.Size(121, 21);
            this.approvedUsersCB.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(41, 35);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(88, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(41, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 20);
            this.textBox1.TabIndex = 3;
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(12, 12);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(23, 13);
            this.fileLabel.TabIndex = 4;
            this.fileLabel.Text = "File";
            // 
            // outputFilenameLabel
            // 
            this.outputFilenameLabel.AutoSize = true;
            this.outputFilenameLabel.Location = new System.Drawing.Point(12, 84);
            this.outputFilenameLabel.Name = "outputFilenameLabel";
            this.outputFilenameLabel.Size = new System.Drawing.Size(81, 13);
            this.outputFilenameLabel.TabIndex = 5;
            this.outputFilenameLabel.Text = "Output filename";
            // 
            // outputFilenameTB
            // 
            this.outputFilenameTB.Location = new System.Drawing.Point(108, 81);
            this.outputFilenameTB.Name = "outputFilenameTB";
            this.outputFilenameTB.Size = new System.Drawing.Size(100, 20);
            this.outputFilenameTB.TabIndex = 6;
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(146, 160);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 7;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            // 
            // decryptMessageLabel
            // 
            this.decryptMessageLabel.AutoSize = true;
            this.decryptMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.decryptMessageLabel.Location = new System.Drawing.Point(12, 144);
            this.decryptMessageLabel.Name = "decryptMessageLabel";
            this.decryptMessageLabel.Size = new System.Drawing.Size(97, 13);
            this.decryptMessageLabel.TabIndex = 8;
            this.decryptMessageLabel.Text = "Incorrect password";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 209);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(240, 23);
            this.progressBar.TabIndex = 9;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(12, 119);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(120, 13);
            this.passwordLabel.TabIndex = 10;
            this.passwordLabel.Text = "Password (chosen user)";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(138, 116);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(100, 20);
            this.passwordTB.TabIndex = 11;
            // 
            // DecryptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 245);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.decryptMessageLabel);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.outputFilenameTB);
            this.Controls.Add(this.outputFilenameLabel);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.approvedUsersCB);
            this.Controls.Add(this.approvedUsersLabel);
            this.Name = "DecryptForm";
            this.Text = "File decryption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label approvedUsersLabel;
        private System.Windows.Forms.ComboBox approvedUsersCB;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Label outputFilenameLabel;
        private System.Windows.Forms.TextBox outputFilenameTB;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Label decryptMessageLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTB;
    }
}
namespace encryptionFilesAES
{
    partial class EncryptForm
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
            this.encryptionModeLabel = new System.Windows.Forms.Label();
            this.encryptionModeCB = new System.Windows.Forms.ComboBox();
            this.fileLabel = new System.Windows.Forms.Label();
            this.fileTB = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.outputFilenameTB = new System.Windows.Forms.TextBox();
            this.outputFilenameLabel = new System.Windows.Forms.Label();
            this.encryptButton = new System.Windows.Forms.Button();
            this.encryptPB = new System.Windows.Forms.ProgressBar();
            this.encryptMessageLabel = new System.Windows.Forms.Label();
            this.approvedUsersLB = new System.Windows.Forms.CheckedListBox();
            this.approvedUsersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // encryptionModeLabel
            // 
            this.encryptionModeLabel.AutoSize = true;
            this.encryptionModeLabel.Location = new System.Drawing.Point(12, 13);
            this.encryptionModeLabel.Name = "encryptionModeLabel";
            this.encryptionModeLabel.Size = new System.Drawing.Size(86, 13);
            this.encryptionModeLabel.TabIndex = 0;
            this.encryptionModeLabel.Text = "Encryption mode";
            // 
            // encryptionModeCB
            // 
            this.encryptionModeCB.FormattingEnabled = true;
            this.encryptionModeCB.Items.AddRange(new object[] {
            "ECB",
            "CCB",
            "CFB",
            "OFB"});
            this.encryptionModeCB.Location = new System.Drawing.Point(113, 10);
            this.encryptionModeCB.Name = "encryptionModeCB";
            this.encryptionModeCB.Size = new System.Drawing.Size(121, 21);
            this.encryptionModeCB.TabIndex = 1;
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(12, 50);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(23, 13);
            this.fileLabel.TabIndex = 2;
            this.fileLabel.Text = "File";
            // 
            // fileTB
            // 
            this.fileTB.ForeColor = System.Drawing.Color.Black;
            this.fileTB.Location = new System.Drawing.Point(41, 47);
            this.fileTB.Name = "fileTB";
            this.fileTB.Size = new System.Drawing.Size(296, 20);
            this.fileTB.TabIndex = 3;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(41, 73);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(88, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.Browse_Click);
            // 
            // outputFilenameTB
            // 
            this.outputFilenameTB.Location = new System.Drawing.Point(113, 118);
            this.outputFilenameTB.Name = "outputFilenameTB";
            this.outputFilenameTB.Size = new System.Drawing.Size(100, 20);
            this.outputFilenameTB.TabIndex = 5;
            // 
            // outputFilenameLabel
            // 
            this.outputFilenameLabel.AutoSize = true;
            this.outputFilenameLabel.Location = new System.Drawing.Point(15, 121);
            this.outputFilenameLabel.Name = "outputFilenameLabel";
            this.outputFilenameLabel.Size = new System.Drawing.Size(81, 13);
            this.outputFilenameLabel.TabIndex = 6;
            this.outputFilenameLabel.Text = "Output filename";
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(113, 160);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 7;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // encryptPB
            // 
            this.encryptPB.Location = new System.Drawing.Point(15, 239);
            this.encryptPB.Name = "encryptPB";
            this.encryptPB.Size = new System.Drawing.Size(247, 23);
            this.encryptPB.TabIndex = 8;
            this.encryptPB.Value = 60;
            // 
            // encryptMessageLabel
            // 
            this.encryptMessageLabel.AutoSize = true;
            this.encryptMessageLabel.ForeColor = System.Drawing.Color.Green;
            this.encryptMessageLabel.Location = new System.Drawing.Point(28, 214);
            this.encryptMessageLabel.Name = "encryptMessageLabel";
            this.encryptMessageLabel.Size = new System.Drawing.Size(111, 13);
            this.encryptMessageLabel.TabIndex = 9;
            this.encryptMessageLabel.Text = "Encryption in progress";
            // 
            // approvedUsersLB
            // 
            this.approvedUsersLB.FormattingEnabled = true;
            this.approvedUsersLB.Location = new System.Drawing.Point(343, 47);
            this.approvedUsersLB.Name = "approvedUsersLB";
            this.approvedUsersLB.Size = new System.Drawing.Size(120, 169);
            this.approvedUsersLB.TabIndex = 10;
            // 
            // approvedUsersLabel
            // 
            this.approvedUsersLabel.AutoSize = true;
            this.approvedUsersLabel.Location = new System.Drawing.Point(340, 13);
            this.approvedUsersLabel.Name = "approvedUsersLabel";
            this.approvedUsersLabel.Size = new System.Drawing.Size(55, 13);
            this.approvedUsersLabel.TabIndex = 11;
            this.approvedUsersLabel.Text = "Receivers";
            // 
            // EncryptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 285);
            this.Controls.Add(this.approvedUsersLabel);
            this.Controls.Add(this.approvedUsersLB);
            this.Controls.Add(this.encryptMessageLabel);
            this.Controls.Add(this.encryptPB);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.outputFilenameLabel);
            this.Controls.Add(this.outputFilenameTB);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileTB);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.encryptionModeCB);
            this.Controls.Add(this.encryptionModeLabel);
            this.Name = "EncryptForm";
            this.Text = "File encryption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label encryptionModeLabel;
        private System.Windows.Forms.ComboBox encryptionModeCB;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.TextBox fileTB;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox outputFilenameTB;
        private System.Windows.Forms.Label outputFilenameLabel;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.ProgressBar encryptPB;
        private System.Windows.Forms.Label encryptMessageLabel;
        private System.Windows.Forms.CheckedListBox approvedUsersLB;
        private System.Windows.Forms.Label approvedUsersLabel;
    }
}
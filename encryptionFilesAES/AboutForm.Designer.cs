namespace encryptionFilesAES
{
    partial class AboutForm
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
            this.infoLabel = new System.Windows.Forms.Label();
            this.authorsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(2, 9);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(259, 39);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Application which encrypts and decrypts files \r\nusing Advanced Encryption Standar" +
    "d, RSA algorithm \r\nand different block cipher modes of operations. ";
            // 
            // authorsLabel
            // 
            this.authorsLabel.AutoSize = true;
            this.authorsLabel.Location = new System.Drawing.Point(68, 81);
            this.authorsLabel.Name = "authorsLabel";
            this.authorsLabel.Size = new System.Drawing.Size(193, 13);
            this.authorsLabel.TabIndex = 1;
            this.authorsLabel.Text = "Michał Kazanowski, Oskar Kowalewski";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 100);
            this.Controls.Add(this.authorsLabel);
            this.Controls.Add(this.infoLabel);
            this.Name = "AboutForm";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label authorsLabel;
    }
}
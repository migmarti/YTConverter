namespace YTConverter
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.directoryTextBox = new System.Windows.Forms.TextBox();
            this.directoryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkTextBox = new System.Windows.Forms.TextBox();
            this.pasteButton = new System.Windows.Forms.Button();
            this.downloadMP3Button = new System.Windows.Forms.Button();
            this.downloadVideoButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.percentageLabel = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.buttonGetTitle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAlbum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save To Directory:";
            // 
            // directoryTextBox
            // 
            this.directoryTextBox.Location = new System.Drawing.Point(113, 10);
            this.directoryTextBox.Name = "directoryTextBox";
            this.directoryTextBox.ReadOnly = true;
            this.directoryTextBox.Size = new System.Drawing.Size(297, 20);
            this.directoryTextBox.TabIndex = 1;
            // 
            // directoryButton
            // 
            this.directoryButton.Location = new System.Drawing.Point(416, 10);
            this.directoryButton.Name = "directoryButton";
            this.directoryButton.Size = new System.Drawing.Size(75, 20);
            this.directoryButton.TabIndex = 2;
            this.directoryButton.Text = "Change";
            this.directoryButton.UseVisualStyleBackColor = true;
            this.directoryButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Youtube Link:";
            // 
            // linkTextBox
            // 
            this.linkTextBox.Location = new System.Drawing.Point(113, 36);
            this.linkTextBox.Name = "linkTextBox";
            this.linkTextBox.Size = new System.Drawing.Size(297, 20);
            this.linkTextBox.TabIndex = 4;
            // 
            // pasteButton
            // 
            this.pasteButton.Location = new System.Drawing.Point(416, 36);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(75, 21);
            this.pasteButton.TabIndex = 5;
            this.pasteButton.Text = "Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // downloadMP3Button
            // 
            this.downloadMP3Button.Location = new System.Drawing.Point(278, 114);
            this.downloadMP3Button.Name = "downloadMP3Button";
            this.downloadMP3Button.Size = new System.Drawing.Size(132, 45);
            this.downloadMP3Button.TabIndex = 6;
            this.downloadMP3Button.Text = "Download and Convert to MP3";
            this.downloadMP3Button.UseVisualStyleBackColor = true;
            this.downloadMP3Button.Click += new System.EventHandler(this.downloadMP3Button_Click);
            // 
            // downloadVideoButton
            // 
            this.downloadVideoButton.Location = new System.Drawing.Point(113, 114);
            this.downloadVideoButton.Name = "downloadVideoButton";
            this.downloadVideoButton.Size = new System.Drawing.Size(132, 45);
            this.downloadVideoButton.TabIndex = 7;
            this.downloadVideoButton.Text = "Download Video";
            this.downloadVideoButton.UseVisualStyleBackColor = true;
            this.downloadVideoButton.Click += new System.EventHandler(this.downloadVideoButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(113, 165);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(297, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // percentageLabel
            // 
            this.percentageLabel.AutoSize = true;
            this.percentageLabel.Location = new System.Drawing.Point(110, 191);
            this.percentageLabel.Name = "percentageLabel";
            this.percentageLabel.Size = new System.Drawing.Size(21, 13);
            this.percentageLabel.TabIndex = 9;
            this.percentageLabel.Text = "0%";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(113, 62);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(297, 20);
            this.textBoxTitle.TabIndex = 10;
            // 
            // buttonGetTitle
            // 
            this.buttonGetTitle.Location = new System.Drawing.Point(416, 62);
            this.buttonGetTitle.Name = "buttonGetTitle";
            this.buttonGetTitle.Size = new System.Drawing.Size(75, 20);
            this.buttonGetTitle.TabIndex = 11;
            this.buttonGetTitle.Text = "Get Title";
            this.buttonGetTitle.UseVisualStyleBackColor = true;
            this.buttonGetTitle.Click += new System.EventHandler(this.buttonGetTitle_ClickAsync);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Override Title:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Set Album Title:";
            // 
            // textBoxAlbum
            // 
            this.textBoxAlbum.Location = new System.Drawing.Point(113, 88);
            this.textBoxAlbum.Name = "textBoxAlbum";
            this.textBoxAlbum.Size = new System.Drawing.Size(297, 20);
            this.textBoxAlbum.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 211);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAlbum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonGetTitle);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.percentageLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.downloadVideoButton);
            this.Controls.Add(this.downloadMP3Button);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.linkTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.directoryButton);
            this.Controls.Add(this.directoryTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "YTConverter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directoryTextBox;
        private System.Windows.Forms.Button directoryButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox linkTextBox;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button downloadMP3Button;
        private System.Windows.Forms.Button downloadVideoButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label percentageLabel;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Button buttonGetTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAlbum;
    }
}


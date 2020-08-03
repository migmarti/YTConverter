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
using Xabe.FFmpeg;
using Xabe.FFmpeg.Enums;
using Xabe.FFmpeg.Model;
using YoutubeExplode;
using YoutubeExtractor;
using YTConverter.Properties;

namespace YTConverter
{
    public partial class Form1 : Form
    {
        string selectedPath;
        YoutubeMediaDownloader ytd; 
        public Form1()
        {
            InitializeComponent();
            ytd = new YoutubeMediaDownloader(progressBar1, percentageLabel);
            selectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            directoryTextBox.Text = selectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                selectedPath = folderBrowser.SelectedPath;
                directoryTextBox.Text = selectedPath;
            }
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                linkTextBox.Text = clipboardText;
            }
        }

        private async void downloadMP3Button_Click(object sender, EventArgs e)
        {
            enableButtons(false);
            await ytd.handleYouTubeMediaDownload(linkTextBox.Text, true, selectedPath, textBoxTitle.Text, textBoxAlbum.Text);
            enableButtons(true);
            textBoxTitle.Text = "";
        }

        private async void downloadVideoButton_Click(object sender, EventArgs e)
        {
            enableButtons(false);
            await ytd.handleYouTubeMediaDownload(linkTextBox.Text, false, selectedPath, textBoxTitle.Text, textBoxAlbum.Text);
            enableButtons(true);
            textBoxTitle.Text = "";
        }

        private async void buttonGetTitle_ClickAsync(object sender, EventArgs e)
        {
            enableButtons(false);
            textBoxTitle.Text = await ytd.getYouTubeMediaTitle(linkTextBox.Text);
            enableButtons(true);
        }

        private void enableButtons(bool enabled)
        {
            downloadMP3Button.Enabled = enabled;
            downloadVideoButton.Enabled = enabled;
            buttonGetTitle.Enabled = enabled;
        }
    }
}

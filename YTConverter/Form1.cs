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
        ProgressIndicator progressIndicator;
        YoutubeMediaDownloader ytd;
        AudioConversionUtils audioConversionUtils;
        public Form1()
        {
            InitializeComponent();
            progressIndicator = new ProgressIndicator(progressBar1, percentageLabel);
            ytd = new YoutubeMediaDownloader(progressIndicator);
            selectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            audioConversionUtils = new AudioConversionUtils(progressIndicator);
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
            string downloadPath = await ytd.handleYouTubeMediaDownload(linkTextBox.Text, selectedPath, textBoxTitle.Text);
            if (downloadPath != "")
            {
                string outputPath = Path.ChangeExtension(downloadPath, FileExtensions.Mp3);
                if (await audioConversionUtils.convertMp4ToMp3(downloadPath, outputPath))
                {
                    audioConversionUtils.addAlbumTag(outputPath, textBoxAlbum.Text);
                    MessageBox.Show("Done converting downloaded video to MP3 at: " + outputPath);
                }
            }
            enableButtons(true);
            textBoxTitle.Text = "";
        }

        private async void downloadVideoButton_Click(object sender, EventArgs e)
        {
            enableButtons(false);
            string downloadPath = await ytd.handleYouTubeMediaDownload(linkTextBox.Text, selectedPath, textBoxTitle.Text);
            if (downloadPath != "")
            {
                MessageBox.Show("Done downloading video:\n" + textBoxTitle.Text + "\nAt:\n" + downloadPath);
            }
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
            progressIndicator.updateProgress("", 0);
        }
    }
}

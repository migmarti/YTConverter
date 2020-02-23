using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Enums;
using Xabe.FFmpeg.Model;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace YTConverter
{
    class YoutubeMediaDownloader
    {
        private ProgressBar progressBar;
        private Label percentLabel;
        public YoutubeMediaDownloader(ProgressBar progressBar, Label percentLabel) {
            this.progressBar = progressBar;
            this.percentLabel = percentLabel;
        }

        private void updateProgress(string text, int percent)
        {
            if (ControlInvokeRequired(progressBar, () => updateProgress(text, percent))) return;
            if (ControlInvokeRequired(percentLabel, () => updateProgress(text, percent))) return;
            progressBar.Value = percent;
            percentLabel.Text = text + percent + "%";
        }

        private static bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;
            return true;
        }

        public async Task<string> getYouTubeMediaTitle(string url)
        {
            try
            {
                updateProgress("Getting video data: ", 0);
                var id = YoutubeClient.ParseVideoId(url);
                var client = new YoutubeClient();
                var video = await client.GetVideoAsync(id);
                var title = video.Title.ToString();
                title = replaceInvalidChars(title);
                updateProgress("Video data retrieved: ", 100);
                return title;
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                updateProgress("", 0);
            }        
            return "";
        }

        public async Task handleYouTubeMediaDownload(string url, bool asMP3, string selectedPath, string title)
        {
            updateProgress("Preparing Stream Information: ", 0);
            try
            {
                var id = YoutubeClient.ParseVideoId(url);
                var client = new YoutubeClient();
                if (title == "") {
                    title = await getYouTubeMediaTitle(url);
                }
                title = replaceInvalidChars(title);
                var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);
                var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
                var ext = streamInfo.Container.GetFileExtension().ToString();
                string downloadPath = $"{selectedPath}\\{title}.{ext}";
                await downloadYouTubeMedia(client, streamInfo, title, downloadPath);
                if (asMP3)
                {
                    await convertMp4ToMp3(downloadPath, title);
                    MessageBox.Show("Done converting downloaded video to MP3: " + title + "\nAt: " + Path.ChangeExtension(downloadPath, FileExtensions.Mp3));
                }
                else
                {
                    MessageBox.Show("Done downloading video:\n" + title + "\nAt:\n" + downloadPath);
                }

            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                updateProgress("", 0);
            }
        }

        private async Task downloadYouTubeMedia(YoutubeClient client, MediaStreamInfo streamInfo, string title, string downloadPath)
        {
            var progress = new Progress<double>(p =>
            {
                updateProgress("Downloading Video: ", (int)((p - (int)p) * 100));
            });
            await client.DownloadMediaStreamAsync(streamInfo, downloadPath, progress);
            updateProgress("Video Download Done! ", 100);
        }

        private async Task convertMp4ToMp3(string mp4File, string title)
        {
            string output = Path.ChangeExtension(mp4File, FileExtensions.Mp3);
            try
            {
                IConversion conversion = Conversion.ExtractAudio(mp4File, output);
                conversion.OnProgress += (sender, args) =>
                {
                    int percent = (int)(Math.Round(args.Duration.TotalSeconds / args.TotalLength.TotalSeconds, 2) * 100);
                    updateProgress("Converting to MP3: ", percent);
                };
                IConversionResult result = await conversion.Start();
            }
            catch (Xabe.FFmpeg.Exceptions.ConversionException)
            {
                MessageBox.Show(title + " already exists at " + output);
            }
            File.Delete(mp4File);
            updateProgress("Conversion Done! ", 100);
        }

        public string replaceInvalidChars(string filename)
        {
            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()));
        }

    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Model;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

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
                var client = new YoutubeClient();
                var video = await client.Videos.GetAsync(url);
                var title = video.Title.ToString();
                title = replaceInvalidChars(title);
                updateProgress("Video data retrieved: ", 100);
                return title;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message + "\n\n" + e.Source + "\n\n" + e.StackTrace);
                updateProgress("", 0);
            }        
            return "";
        }

        public async Task handleYouTubeMediaDownload(string url, bool asMP3, string selectedPath, string title, string albumTitle)
        {
            updateProgress("Preparing Stream Information: ", 0);
            try
            {
                var id = getVideoId(url);
                var client = new YoutubeClient();
                if (title == "") {
                    title = await getYouTubeMediaTitle(url);
                }
                title = replaceInvalidChars(title);
                var streamManifest = await client.Videos.Streams.GetManifestAsync(id);
                var streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
                string downloadPath = $"{selectedPath}\\{title}.mp4";
                await downloadYouTubeMedia(client, streamInfo, downloadPath);

                if (asMP3)
                {
                    await convertMp4ToMp3(downloadPath, title, albumTitle);
                    MessageBox.Show("Done converting downloaded video to MP3: " + title + "\nAt: " + downloadPath);
                }
                else
                {
                    MessageBox.Show("Done downloading video:\n" + title + "\nAt:\n" + downloadPath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                updateProgress("", 0);
            }
        }

        private async Task downloadYouTubeMedia(YoutubeClient client, IStreamInfo streamInfo, string downloadPath)
        {
            var progress = new Progress<double>(p =>
            {
                updateProgress("Downloading Media: ", (int)((p - (int)p) * 100));
            });
            await client.Videos.Streams.DownloadAsync(streamInfo, downloadPath, progress);
            updateProgress("Media Download Done! ", 100);
        }

        private async Task convertMp4ToMp3(string mp4File, string title, string albumTitle)
        {
            string output = Path.ChangeExtension(mp4File, Xabe.FFmpeg.Enums.FileExtensions.Mp3);
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
            addAlbumTag(output, albumTitle);
            File.Delete(mp4File);
            updateProgress("Conversion Done! ", 100);
        }

        private void addAlbumTag(String path, String albumTitle)
        {
            if (albumTitle != "")
            {
                TagLib.File f = TagLib.File.Create(path);
                f.Tag.Album = albumTitle;
                f.Save();
            }
        }

        private String getVideoId(String url)
        {
            Uri uri = new Uri(url);
            return HttpUtility.ParseQueryString(uri.Query).Get("v");
        }

        public string replaceInvalidChars(string filename)
        {
            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()));
        }

    }
}

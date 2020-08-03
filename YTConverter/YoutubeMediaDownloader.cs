using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Xabe.FFmpeg.Enums;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YTConverter
{
    class YoutubeMediaDownloader
    {
        ProgressIndicator indicator;
        public YoutubeMediaDownloader(ProgressIndicator progressIndicator) {
            this.indicator = progressIndicator;
        }

        public async Task<string> getYouTubeMediaTitle(string url)
        {
            try
            {
                indicator.updateProgress("Getting video data: ", 0);
                var client = new YoutubeClient();
                var video = await client.Videos.GetAsync(url);
                var title = video.Title.ToString();
                title = replaceInvalidChars(title);
                indicator.updateProgress("Video data retrieved: ", 100);
                return title;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message);
                indicator.updateProgress("", 0);
            }        
            return "";
        }

        public async Task<string> handleYouTubeMediaDownload(string url, bool asMP3, string selectedPath, string title)
        {
            indicator.updateProgress("Preparing Stream Information: ", 0);
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
                return downloadPath;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message);
                indicator.updateProgress("", 0);
                return "";
            }
        }

        private async Task downloadYouTubeMedia(YoutubeClient client, IStreamInfo streamInfo, string downloadPath)
        {
            var progress = new Progress<double>(p =>
            {
                indicator.updateProgress("Downloading Media: ", (int)((p - (int)p) * 100));
            });
            await client.Videos.Streams.DownloadAsync(streamInfo, downloadPath, progress);
            indicator.updateProgress("Media Download Done! ", 100);
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

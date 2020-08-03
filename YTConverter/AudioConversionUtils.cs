using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Enums;
using Xabe.FFmpeg.Model;

namespace YTConverter
{
    class AudioConversionUtils
    {
        ProgressIndicator indicator;
        public AudioConversionUtils(ProgressIndicator progressIndicator)
        {
            this.indicator = progressIndicator;
        }

        public async Task<bool> convertMp4ToMp3(string mp4FilePath, string outputFilePath)
        {
            bool success = false;
            try
            {
                IConversion conversion = Conversion.ExtractAudio(mp4FilePath, outputFilePath);
                conversion.OnProgress += (sender, args) =>
                {
                    int percent = (int)(Math.Round(args.Duration.TotalSeconds / args.TotalLength.TotalSeconds, 2) * 100);
                    indicator.updateProgress("Converting to MP3: ", percent);
                };
                IConversionResult result = await conversion.Start();
                File.Delete(mp4FilePath);
                indicator.updateProgress("Conversion Done! ", 100);
                success = true;
            }
            catch (Xabe.FFmpeg.Exceptions.ConversionException e)
            {
                MessageBox.Show("An error has occurred at: " + outputFilePath + "\n" + e.Message);
            }
            return success;
        }

        public void addAlbumTag(string path, string albumTitle)
        {
            if (albumTitle != "")
            {
                TagLib.File f = TagLib.File.Create(path);
                f.Tag.Album = albumTitle;
                f.Save();
            }
        }
    }
}


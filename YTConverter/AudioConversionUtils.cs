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

        public async Task convertMp4ToMp3(string mp4File, string title, string albumTitle)
        {
            string output = Path.ChangeExtension(mp4File, FileExtensions.Mp3);
            try
            {
                IConversion conversion = Conversion.ExtractAudio(mp4File, output);
                conversion.OnProgress += (sender, args) =>
                {
                    int percent = (int)(Math.Round(args.Duration.TotalSeconds / args.TotalLength.TotalSeconds, 2) * 100);
                    indicator.updateProgress("Converting to MP3: ", percent);
                };
                IConversionResult result = await conversion.Start();
            }
            catch (Xabe.FFmpeg.Exceptions.ConversionException)
            {
                MessageBox.Show(title + " already exists at " + output);
            }
            addAlbumTag(output, albumTitle);
            File.Delete(mp4File);
            indicator.updateProgress("Conversion Done! ", 100);
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
    }
}


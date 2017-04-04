using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookBuilder
{
    /// <summary>Stores info for one page of the BB_Book.</summary>
    public class BB_Page
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the page image file for the .armb file.
        /// </summary>
        /// <value>The name of the page image file.</value>
        public string PageImageFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the video file for the .armb file.
        /// </summary>
        /// <value>The name of the video file.</value>
        public string VideoFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the audio file for the .armb file.
        /// </summary>
        /// <value>The name of the audio file.</value>
        public string AudioFileName { get; set; }

        /// <summary>
        /// Gets or sets absolute file path + filename of the source page image file.
        /// </summary>
        /// <value>The absolute file path + filename of the source page image file.</value>
        public string SourcePageImageFileName { get; set; }

        /// <summary>
        /// Gets or sets the absolute file path + filename of the source video file.
        /// </summary>
        /// <value>The absolute file path + filename of the source video file.</value>
        public string SourceVideoFileName { get; set; }

        /// <summary>
        /// Gets or sets the absolute file path + filename of the source audio file.
        /// </summary>
        /// <value>The absolute file path + filename of the source audio file.</value>
        public string SourceAudioFileName { get; set; }

        /// <summary>
        /// Gets or sets the width of the video.
        /// </summary>
        /// <value>The width of the video.</value>
        public int VideoWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the video.
        /// </summary>
        /// <value>The height of the video.</value>
        public int VideoHeight { get; set; }

        /// <summary>
        /// Gets or sets the x coordinate of the video.
        /// </summary>
        /// <value>The x coordinate of the video.</value>
        public int VideoX { get; set; }

        /// <summary>
        /// Gets or sets the y coordinate of the video.
        /// </summary>
        /// <value>The y coordinate of the video.</value>
        public int VideoY { get; set; }

        /// <summary>
        /// Gets or sets the video MD5 hash value.
        /// </summary>
        /// <value>The video MD5 hash value.</value>
        public string VideoMD5 { get; set; }

        /// <summary>
        /// Gets or sets the audio MD5 hash value.
        /// </summary>
        /// <value>The audio MD5 hash value.</value>
        public string AudioMD5 { get; set; }

        /// <summary>Tries to open a file and returns its MD5 hash value as a string.</summary>
        /// <param name="filename">The name of the file to be opened.</param>
        /// <returns>the MD5 hash as a string.</returns>
        public static string GetMD5(String filename)
        {
            MD5 md5 = MD5.Create();
            String hash = "";
            //Open the file, compute the hash
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                foreach (byte b in md5.ComputeHash(fs))
                {
                    hash += b.ToString("x2").ToLower();
                }
            }
            return hash;
        }
    }
}

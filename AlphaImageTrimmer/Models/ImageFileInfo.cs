using System.IO;

namespace AlphaImageTrimmer.Models
{
    public class ImageFileInfo
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public FileInfo FileInfo { get; set; }

        public bool Edited { get; set; }
    }
}
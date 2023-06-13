using System.IO;
using Prism.Mvvm;

namespace AlphaImageTrimmer.Models
{
    public class ImageFileInfo : BindableBase
    {
        private int width;
        private int height;
        private int x;
        private int y;

        public ImageFileInfo(FileInfo f)
        {
            FileInfo = f;
        }

        public int Width { get => width; set => SetProperty(ref width, value); }

        public int Height { get => height; set => SetProperty(ref height, value); }

        public FileInfo FileInfo { get; set; }

        public int X { get => x; set => SetProperty(ref x, value); }

        public int Y { get => y; set => SetProperty(ref y, value); }
    }
}
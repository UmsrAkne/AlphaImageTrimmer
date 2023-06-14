using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using AlphaImageTrimmer.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace AlphaImageTrimmer.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private List<ImageFileInfo> bitmapFiles;

        public string Title { get => title; set => SetProperty(ref title, value); }

        public List<ImageFileInfo> BitmapFiles
        {
            get => bitmapFiles;
            set
            {
                SetProperty(ref bitmapFiles, value);
                RaisePropertyChanged(nameof(CanCrop));
            }
        }

        public bool CanCrop
        {
            get
            {
                return BitmapFiles != null && BitmapFiles.Count > 0;
            }
        }

        public DelegateCommand CropCommand => new DelegateCommand(() =>
        {
            if (BitmapFiles == null || BitmapFiles.Count == 0)
            {
                return;
            }

            foreach (var bf in BitmapFiles)
            {
                var bmp = new Bitmap(bf.FileInfo.FullName);
                var fileName = $@"{bf.FileInfo.DirectoryName}\{Path.GetFileNameWithoutExtension(bf.FileInfo.FullName)}_cut{bf.FileInfo.Extension}";
                var rect = Trimmer.GetRect(bmp);

                bf.Width = rect.Width;
                bf.Height = rect.Height;
                bf.X = rect.X;
                bf.Y = rect.Y;

                Trimmer.Crop(bmp, rect, fileName);
            }
        });

        public DelegateCommand CopyToClipboardCommand => new DelegateCommand(() =>
        {
            var sb = new StringBuilder();

            sb.AppendLine("<root>");
            foreach (ImageFileInfo m in BitmapFiles)
            {
                var x = $@"{nameof(ImageFileInfo.X)}=""{m.X}""";
                var y = $@"{nameof(ImageFileInfo.Y)}=""{m.Y}""";
                var w = $@"{nameof(ImageFileInfo.Width)}=""{m.Height}""";
                var h = $@"{nameof(ImageFileInfo.Height)}=""{m.Height}""";
                sb.AppendLine($"\t<image {x} {y} {w} {h} />");
            }

            sb.AppendLine("</root>");
            Clipboard.SetDataObject(sb.ToString());
        });
    }
}
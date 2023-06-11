using System.Collections.Generic;
using System.Drawing;
using System.IO;
using AlphaImageTrimmer.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace AlphaImageTrimmer.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private List<FileInfo> bitmapFiles;

        public string Title { get => title; set => SetProperty(ref title, value); }

        public List<FileInfo> BitmapFiles { get => bitmapFiles; set => SetProperty(ref bitmapFiles, value); }

        public DelegateCommand CropCommand => new DelegateCommand(() =>
        {
            if (BitmapFiles == null || BitmapFiles.Count == 0)
            {
                return;
            }

            foreach (var bf in BitmapFiles)
            {
                var bmp = new Bitmap(bf.FullName);
                var fileName = $@"{bf.DirectoryName}\{Path.GetFileNameWithoutExtension(bf.FullName)}_cut{bf.Extension}";
                var rect = Trimmer.GetRect(bmp);
                Trimmer.Crop(bmp, rect, fileName);
            }
        });
    }
}
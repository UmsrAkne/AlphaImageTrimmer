using System.Collections.Generic;
using System.IO;
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
    }
}
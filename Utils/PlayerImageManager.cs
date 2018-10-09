using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ProjectCarsSeasonExtension.Views.Player;

namespace ProjectCarsSeasonExtension.Utils
{
    internal sealed class PlayerImageManager
    {
        public event Action<float> LoadingProgressChanged;

        public ObservableCollection<SelectableImage> FilteredImages { get; set; } = new ObservableCollection<SelectableImage>();
        private readonly List<SelectableImage> _allImages = new List<SelectableImage>();

        private static readonly Lazy<PlayerImageManager> Lazy =
            new Lazy<PlayerImageManager>(() => new PlayerImageManager());

        private string[] _playerImagePaths;
        
        public static PlayerImageManager Instance => Lazy.Value;

        public PlayerImageManager()
        {
            StartImageLoadingBackgroundWorker();
        }

        private void StartImageLoadingBackgroundWorker()
        {
            BackgroundWorker worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;

            worker.RunWorkerCompleted += AllImages_LoadingCompleted;

            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!(sender is BackgroundWorker backgroundWorker))
                return;

            LoadAllImages(backgroundWorker);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadingProgressChanged?.Invoke(e.ProgressPercentage);
        }

        private void AllImages_LoadingCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FilterImages(PlayerImageFolders.Avatars);
        }

        private void LoadAllImages(BackgroundWorker backgroundWorker)
        {
            var allPlayerImagesPath = Environment.CurrentDirectory + @"\Assets\Players\";

            if (Directory.Exists(allPlayerImagesPath))
                _playerImagePaths = Directory.GetFiles(allPlayerImagesPath, "*.png", SearchOption.AllDirectories);
            else
                return;

            for (var i = 0; i < _playerImagePaths.Length; i++)
            {
                var playerImagePath = _playerImagePaths[i];

                if(Application.Current == null || Application.Current.Dispatcher == null)
                    return;

                Application.Current.Dispatcher.Invoke(delegate 
                {
                    var image = new Image
                    {
                        Source = new BitmapImage(new Uri(playerImagePath, UriKind.Absolute))
                    };
                
                    _allImages.Add(new SelectableImage
                    {
                        Image = image,
                        Path = playerImagePath
                    });
                });

                int percentProgress = (int)Math.Ceiling(((float)(i * 100)) / (_playerImagePaths.Length * 100));
                backgroundWorker.ReportProgress(percentProgress);
            }
        }

        public void FilterImages(PlayerImageFolders filter)
        {
            BackgroundWorker filteringWorker = new BackgroundWorker { WorkerReportsProgress = true };
            filteringWorker.DoWork += Worker_FilterImages;
            filteringWorker.ProgressChanged += Worker_ProgressChanged;
            filteringWorker.RunWorkerAsync(filter);
        }

        private void Worker_FilterImages(object sender, DoWorkEventArgs e)
        {
            if (!(sender is BackgroundWorker filteringWorker))
                return;

            PlayerImageFolders filter = (PlayerImageFolders)e.Argument;

            Application.Current.Dispatcher.Invoke(delegate
            {
                FilteredImages.Clear();
            });

            for (var i = 0; i < _allImages.Count; i++)
            {
                var selectableImage = _allImages[i];
                if (selectableImage.Path.Contains(filter.ToString()))
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        FilteredImages.Add(selectableImage);
                    });
                }

                int percentProgress = (int)Math.Ceiling(((float)i / _allImages.Count) * 100);
                filteringWorker.ReportProgress(percentProgress);
            }
        }
    }
}

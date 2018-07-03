using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ProjectCarsSeasonExtension.Views.Player;

namespace ProjectCarsSeasonExtension.Utils
{
    internal sealed class PlayerImageManager
    {
        public ObservableCollection<SelectableImage> Images { get; set; } = new ObservableCollection<SelectableImage>();

        private static readonly Lazy<PlayerImageManager> Lazy =
            new Lazy<PlayerImageManager>(() => new PlayerImageManager());

        private string[] _playerImagePaths;

        public static PlayerImageManager Instance => Lazy.Value;

        public PlayerImageManager()
        {
            LoadImages();
        }

        private void LoadImages()
        {
            var allPlayerImagesPath = Environment.CurrentDirectory + @"\Assets\Players\";

            if (Directory.Exists(allPlayerImagesPath))
                _playerImagePaths = Directory.GetFiles(allPlayerImagesPath, "*.png", SearchOption.AllDirectories);
            else
                return;

            foreach (var playerImagePath in _playerImagePaths)
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(playerImagePath, UriKind.Absolute))
                };

                Images.Add(new SelectableImage
                {
                    Image = image,
                    Name = Path.GetFileNameWithoutExtension(playerImagePath)
                });
            }
        }
    }
}

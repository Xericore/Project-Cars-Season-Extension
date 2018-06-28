using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ProjectCarsSeasonExtension.Views.Player
{
    /// <summary>
    /// Interaction logic for ImageSelectionWindow.xaml
    /// </summary>
    public partial class ImageSelectionWindow : Window
    {
        public ObservableCollection<Image> Images { get; set; } = new ObservableCollection<Image>();

        private string[] _playerImagePaths;

        public ImageSelectionWindow()
        {
            InitializeComponent();

            LoadImages();
        }

        private void LoadImages()
        {
            var allPlayerImagesPath = Environment.CurrentDirectory + @"\Assets\Players\";

            if (Directory.Exists(allPlayerImagesPath))
                _playerImagePaths = Directory.GetFiles(allPlayerImagesPath, "*.png");
            else
                return;

            foreach (var playerImagePath in _playerImagePaths)
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(playerImagePath, UriKind.Absolute))
                };
                Images.Add(image);
            }

        }

        private void ImageSelected_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }

    public class SelectableImage
    {
        public string Uri { get; set; }
        public string Name { get; set; }
    }
}

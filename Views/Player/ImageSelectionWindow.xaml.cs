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
        public ObservableCollection<SelectableImage> Images { get; set; } = new ObservableCollection<SelectableImage>();

        public Image SelectedImage { get; set; }

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
                Images.Add(new SelectableImage
                {
                    Image = image,
                    Name = Path.GetFileNameWithoutExtension(playerImagePath)
                });
            }
        }

        private void ImageSelected_OnClick(object sender, RoutedEventArgs e)
        {
            var dataContext = (sender as Button)?.DataContext;
            SelectedImage = (dataContext as SelectableImage)?.Image;
            DialogResult = true;
        }
    }

    public class SelectableImage
    {
        public Image Image { get; set; }
        public string Name { get; set; }
    }
}

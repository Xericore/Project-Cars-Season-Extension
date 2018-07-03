using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Views.Player
{
    /// <summary>
    /// Interaction logic for ImageSelectionWindow.xaml
    /// </summary>
    public partial class ImageSelectionWindow : Window
    {
        public ObservableCollection<SelectableImage> Images => PlayerImageManager.Instance.Images;

        public Image SelectedImage { get; set; }
        
        public ImageSelectionWindow()
        {
            InitializeComponent();
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

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<SelectableImage> Images => PlayerImageManager.Instance.FilteredImages;

        public Image SelectedImage { get; set; }

        private static readonly Lazy<ImageSelectionWindow> Lazy =
            new Lazy<ImageSelectionWindow>(() => new ImageSelectionWindow());

        public static ImageSelectionWindow Instance => Lazy.Value;

        private bool _forceClosing;

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

        private void ImageSelectionWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (_forceClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        public void CloseForced()
        {
            _forceClosing = true;
            Close();
        }


        private void ImageTypeSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;

            if (!(e.AddedItems[0] is PlayerImageFolders selectedFolder))
                return;

            PlayerImageManager.Instance.FilterImages(selectedFolder);
        }
    }
}

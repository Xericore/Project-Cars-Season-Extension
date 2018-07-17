using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Views.Player
{
    /// <summary>
    /// Interaction logic for ImageSelectionWindow.xaml
    /// </summary>
    public partial class ImageSelectionWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<SelectableImage> Images => PlayerImageManager.Instance.FilteredImages;

        public Image SelectedImage { get; set; }

        private static readonly Lazy<ImageSelectionWindow> Lazy =
            new Lazy<ImageSelectionWindow>(() => new ImageSelectionWindow());

        public static ImageSelectionWindow Instance => Lazy.Value;

        public float LoadingProgress { get; set; }

        private bool _forceClosing;

        public ImageSelectionWindow()
        {
            InitializeComponent();

            PlayerImageManager.Instance.LoadingProgressChanged += PlayerImageManager_OnLoadingProgressChanged;
        }

        private void PlayerImageManager_OnLoadingProgressChanged(float progress)
        {
            LoadingProgress = progress;
            OnPropertyChanged(nameof(LoadingProgress));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

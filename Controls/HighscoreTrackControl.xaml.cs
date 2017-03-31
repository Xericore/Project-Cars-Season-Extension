using System.Windows.Controls;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Controls
{
    /// <summary>
    /// Interaction logic for HighscoreTrackControl.xaml
    /// </summary>
    public partial class HighscoreTrackControl : UserControl
    {
        // ----------------------------------------------------------------------------------------

//        public static readonly DependencyProperty TitleProperty =
//            DependencyProperty.Register(
//                "Title", typeof(string),
//                typeof(HighscoreTrackControl),
//                new PropertyMetadata("Track Name")
//            );
//
//        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
//            "Description", typeof(string),
//            typeof(HighscoreTrackControl),
//            new PropertyMetadata("Track Description"));
//
//        public ObservableCollection<PlayerTimeListItemModel> Player { get; set; }

        // ----------------------------------------------------------------------------------------

        public HighscoreTrackControl(TrackViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

//        public string Title
//        {
//            get { return (string) GetValue(TitleProperty); }
//            set { SetValue(TitleProperty, value); }
//        }
//
//        public string Description
//        {
//            get { return (string) GetValue(DescriptionProperty); }
//            set { SetValue(DescriptionProperty, value); }
//        }

        // ----------------------------------------------------------------------------------------
    }
}
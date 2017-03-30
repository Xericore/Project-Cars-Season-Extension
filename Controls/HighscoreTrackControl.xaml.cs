using System.Windows;
using System.Windows.Controls;

namespace ProjectCarsSeasonExtension.Controls
{
    /// <summary>
    /// Interaction logic for HighscoreTrackControl.xaml
    /// </summary>
    public partial class HighscoreTrackControl : UserControl
    {
        // ----------------------------------------------------------------------------------------

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string),
                typeof(HighscoreTrackControl),
                new PropertyMetadata("Track Name")
            );

        // ----------------------------------------------------------------------------------------

        public HighscoreTrackControl()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // ----------------------------------------------------------------------------------------
    }
}
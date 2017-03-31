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

        public HighscoreTrackControl(TrackViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        // ----------------------------------------------------------------------------------------
    }
}
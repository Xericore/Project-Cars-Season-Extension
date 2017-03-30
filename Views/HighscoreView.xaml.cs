using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for HighscoreView.xaml
    /// </summary>
    public partial class HighscoreView : Page
    {
        // ----------------------------------------------------------------------------------------

        public HighscoreView(ObservableCollection<TrackViewModel> trackViewModels)
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------------------------

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            
        }

        // ----------------------------------------------------------------------------------------
    }
}
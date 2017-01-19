using ProjectCarsSeasonExtension.ViewModels;
using System;
using System.Windows.Controls;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for HighscoreView.xaml
    /// </summary>
    public partial class HighscoreView : Page
    {
        // ----------------------------------------------------------------------------------------

        public HighscoreView(HighscoreViewModel _viewModel)
        {
            DataContext = _viewModel;
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

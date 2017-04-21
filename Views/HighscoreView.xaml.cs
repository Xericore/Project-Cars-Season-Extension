using System;
using System.Windows;
using System.Windows.Controls;
using Ninject;
using ProjectCarsSeasonExtension.Controls;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for HighscoreView.xaml
    /// </summary>
    public partial class HighscoreView : Page
    {
        // ----------------------------------------------------------------------------------------

        private readonly HighscoreViewModel _viewModel;

        // ----------------------------------------------------------------------------------------
        [Inject]
        public HighscoreView(HighscoreViewModel highscoreViewModel)
        {
            _viewModel = highscoreViewModel;
            InitializeComponent();
        }

        // ----------------------------------------------------------------------------------------

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // TODO STH: the whole thing needs to be in a separate method and also triggered when the list got changed.
            int rowCount = 1;
            int columnCount = 1;

            // TODO MPE: if you like you could create a nice formula which is doing the following row / column calculation.
            if (_viewModel.TrackViewModels.Count <= 2)
            {
                rowCount = 1;
                columnCount = _viewModel.TrackViewModels.Count;
            }
            else if (_viewModel.TrackViewModels.Count > 2 && _viewModel.TrackViewModels.Count <= 6)
            {
                rowCount = 2;
                columnCount = (int) Math.Ceiling(_viewModel.TrackViewModels.Count / 2.0);
            }
            else if (_viewModel.TrackViewModels.Count > 6 && _viewModel.TrackViewModels.Count <= 12)
            {
                rowCount = 3;
                columnCount = (int) Math.Ceiling(_viewModel.TrackViewModels.Count / 3.0);
            }
            else
            {
                throw new Exception("Such a big one does not fit in here! ;-)");
            }

            for (int y = 0; y < rowCount; y++)
                TrackGrid.RowDefinitions.Add(new RowDefinition());

            for (int x = 0; x < columnCount; x++)
                TrackGrid.ColumnDefinitions.Add(new ColumnDefinition());

            int currentRow = 0;
            int currentColumn = 0;
            foreach (TrackViewModel trackViewModel in _viewModel.TrackViewModels)
            {
                HighscoreTrackControl control =
                    new HighscoreTrackControl(trackViewModel) {Margin = new Thickness(0, 0, 0, 0)};
                Grid.SetRow(control, currentRow);
                Grid.SetColumn(control, currentColumn);
                TrackGrid.Children.Add(control);

                currentColumn++;

                if (currentColumn < columnCount) continue;
                currentRow++;
                currentColumn = 0;
            }
        }

        // ----------------------------------------------------------------------------------------
    }
}
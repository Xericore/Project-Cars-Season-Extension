using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
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

        public HighscoreView(ObservableCollection<TrackViewModel> trackViewModels)
        {
            InitializeComponent();

            var model = new TrackViewModel
            {
                Name = "First Track",
                Description = "Das ist das Haus vom Nikolaus!"
            };

            var player1 = new PlayerTimeListItemModel
            {
                Name = "Mario",
                Position = 1,
                Time = new DateTime().AddMinutes(1).AddSeconds(39).AddMilliseconds(235)
            };
            var player2 = new PlayerTimeListItemModel
            {
                Name = "Simone",
                Position = 2,
                Time = new DateTime().AddMinutes(1).AddSeconds(40).AddMilliseconds(120)
            };
            var player3 = new PlayerTimeListItemModel
            {
                Name = "Sascha",
                Position = 3,
                Time = new DateTime().AddMinutes(1).AddSeconds(53).AddMilliseconds(643)
            };

            model.Player.Add(player1);
            model.Player.Add(player2);
            model.Player.Add(player3);

            TrackContainer.Child = new HighscoreTrackControl(model);
        }

        // ----------------------------------------------------------------------------------------

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        // ----------------------------------------------------------------------------------------
    }
}
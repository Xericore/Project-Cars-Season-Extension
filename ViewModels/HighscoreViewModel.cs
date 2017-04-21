using System;
using System.Collections.ObjectModel;

namespace ProjectCarsSeasonExtension.ViewModels
{
    // --------------------------------------------------------------------------------------------

    public class HighscoreViewModel
    {
        // ----------------------------------------------------------------------------------------

        public ObservableCollection<TrackViewModel> TrackViewModels { get; set; } =
            new ObservableCollection<TrackViewModel>();

        // ----------------------------------------------------------------------------------------

        public HighscoreViewModel()
        {
            // just generating some dummy data to test
            Random trackRand = new Random();
            Random playerRand = new Random();

            int jRand = trackRand.Next(1, 12);
            for (int j = 0; j < jRand; j++)
            {
                TrackViewModel trackModel = new TrackViewModel
                {
                    Name = $"Track {j}",
                    Description = $"Description for Track {j}"
                };

                // add some player
                int pRand = playerRand.Next(0, 6);
                for (int i = 0; i < pRand; i++)
                {
                    PlayerTimeListItemModel player = new PlayerTimeListItemModel
                    {
                        Position = i + 1,
                        Name = $"Player {i}",
                        Time = new DateTime().AddMinutes(1).AddSeconds(i * 10).AddMilliseconds(123)
                    };
                    trackModel.Player.Add(player);
                }

                TrackViewModels.Add(trackModel);
            }
        }
    }

    // --------------------------------------------------------------------------------------------
}
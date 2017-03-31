using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Models;

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
            var trackRand = new Random();
            var playerRand = new Random();

            var jRand = trackRand.Next(1, 12);
            for (var j = 0; j < jRand; j++)
            {
                var trackModel = new TrackViewModel
                {
                    Name = $"Track {j}",
                    Description = $"Description for Track {j}"
                };

                // add some player
                var pRand = playerRand.Next(0, 6);
                for (var i = 0; i < pRand; i++)
                {
                    var player = new PlayerTimeListItemModel
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
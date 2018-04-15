using System.Collections.ObjectModel;

namespace ProjectCarsSeasonExtension.Models
{
    public class DataView
    {
        public SeasonModel CurrentSeason { get; }
        public ObservableCollection<PlayerResult> PlayerResults { get; }
        public ObservableCollection<PlayerModel> Players { get; set; }

        public DataView(SeasonModel currentSeason,
            ObservableCollection<PlayerResult> playerResults,
            ObservableCollection<PlayerModel> players)
        {
            CurrentSeason = currentSeason;
            PlayerResults = playerResults;
            Players = players;
        }
    }
}

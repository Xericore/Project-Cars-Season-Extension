using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Serialization;

namespace ProjectCarsSeasonExtension.Models
{
    public class DataView
    {
        public SeasonModel CurrentSeason { get; }
        public ObservableCollection<PlayerResult> PlayerResults { get; }
        public ObservableCollection<PlayerModel> Players { get; set; }

        public DataView(ISeasonReader seasonReader)
        {
            CurrentSeason = seasonReader.GetCurrentSeason();
            PlayerResults = seasonReader.GetPlayerResults();
            Players = seasonReader.GetPlayers();
        }
    }
}

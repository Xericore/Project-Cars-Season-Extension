using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public interface ISeasonReader
    {
        SeasonModel GetCurrentSeason();
        ObservableCollection<Player> GetPlayers();
        ObservableCollection<PlayerResult> GetPlayerResults();
        ObservableCollection<PlayerHandicap> GetPlayerHandicaps();
    }
}

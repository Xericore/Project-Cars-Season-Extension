using System.Collections.ObjectModel;
using System.Linq;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChampionshipStanding
    {
        public PlayerModel Player { get; set; }

        public ObservableCollection<int> ChallengePoints { get; } = new ObservableCollection<int>();

        public int TotalPoints => ChallengePoints.Sum();

        public ChampionshipStanding(PlayerModel playerName)
        {
            Player = playerName;
        }
    }
}
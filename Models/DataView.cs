using System;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Serialization;

namespace ProjectCarsSeasonExtension.Models
{
    public class DataView
    {
        public ObservableCollection<PlayerResult> PlayerResults { get; }
        public ObservableCollection<Player.Player> Players { get; set; }

        public SeasonModel CurrentSeason { get; }
        public ObservableCollection<PlayerHandicap> Handicaps { get; }

        public DataView(ISeasonReader seasonReader)
        {
            CurrentSeason = seasonReader.GetCurrentSeason();
            PlayerResults = seasonReader.GetPlayerResults();
            Players = seasonReader.GetPlayers();
            Handicaps = seasonReader.GetPlayerHandicaps();
        }

        public TimeSpan GetPlayerHandicap(Player.Player player)
        {
            var foundPlayer = Handicaps.FirstOrDefault(p => p.PlayerId == player.Id);

            return foundPlayer?.Handicap ?? new TimeSpan(0);
        }

        public Challenge GetChallengeById(int challengeId)
        {
            return CurrentSeason?.GetChallengeById(challengeId);
        }
    }
}

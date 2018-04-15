using System;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectCarsSeasonExtension.Serialization;

namespace ProjectCarsSeasonExtension.Models
{
    public class DataView
    {
        public ObservableCollection<PlayerResult> PlayerResults { get; }
        public ObservableCollection<PlayerModel> Players { get; set; }

        private readonly SeasonModel _currentSeason;
        private readonly ObservableCollection<PlayerHandicap> _handicaps;

        public DataView(ISeasonReader seasonReader)
        {
            _currentSeason = seasonReader.GetCurrentSeason();
            PlayerResults = seasonReader.GetPlayerResults();
            Players = seasonReader.GetPlayers();
            _handicaps = seasonReader.GetPlayerHandicaps();
        }

        public TimeSpan GetPlayerHandicap(PlayerModel player)
        {
            var foundPlayer = _handicaps.FirstOrDefault(p => p.PlayerId == player.Id);

            return foundPlayer?.Handicap ?? new TimeSpan(0);
        }

        public Challenge GetChallengeById(int challengeId)
        {
            return _currentSeason?.GetChallengeById(challengeId);
        }
    }
}

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

        public void AddChallengeResult(int playerId, ChallengeResult challengeResult)
        {
            if (DateTime.Now > CurrentSeason.EndDate)
                return;

            Challenge foundChallenge = CurrentSeason.Challenges.FirstOrDefault
            (
                c => c.TrackName == challengeResult.TrackLocationAndVariant &&
                     c.CarName == challengeResult.CarName
            );

            if (foundChallenge == null)
                return;

            PlayerResult foundPlayerResult = PlayerResults.FirstOrDefault(p => p.ChallengeId == foundChallenge.Id && p.PlayerId == playerId);

            if (foundPlayerResult == null)
            {
                PlayerResults.Add(new PlayerResult
                {
                    PlayerId = playerId,
                    ChallengeId = foundChallenge.Id,
                    FastestLap = challengeResult.LastValidLapTime
                });
            }
            else
            {
                if (challengeResult.LastValidLapTime < foundPlayerResult.FastestLap)
                    foundPlayerResult.FastestLap = challengeResult.LastValidLapTime;
            }
        }
    }
}

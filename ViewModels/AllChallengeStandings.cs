using System.Collections.Generic;
using System.Linq;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class AllChallengeStandings
    {
        private readonly DataView _dataView;
        public Dictionary<int, ChallengeStanding> ChallengeStandings { get; } = new Dictionary<int, ChallengeStanding>();

        public AllChallengeStandings(DataView dataView)
        {
            _dataView = dataView;
            CreateChallengeStandings();
        }

        private void CreateChallengeStandings()
        {
            ChallengeStandings.Clear();

            foreach (PlayerResult playerResult in _dataView.PlayerResults)
            {
                if (!_dataView.CurrentSeason.ContainsChallenge(playerResult.ChallengeId))
                    continue;

                if (!ChallengeStandings.ContainsKey(playerResult.ChallengeId))
                {
                    Challenge challenge = _dataView.GetChallengeById(playerResult.ChallengeId);
                    ChallengeStanding challengeStanding = new ChallengeStanding(challenge);
                    ChallengeStandings.Add(playerResult.ChallengeId, challengeStanding);
                }

                Player foundPlayer = _dataView.Players.FirstOrDefault(p => p.Id == playerResult.PlayerId);

                if (foundPlayer != null)
                {
                    SetChallengeStanding(playerResult, foundPlayer);
                }
            }

            AddChallengeStandingsWithoutPlayerResults();
        }

        private void AddChallengeStandingsWithoutPlayerResults()
        {
            if (_dataView.CurrentSeason == null)
                return;

            foreach (var currentSeasonChallengeId in _dataView.CurrentSeason.ChallengeIds)
            {
                if (!ChallengeStandings.ContainsKey(currentSeasonChallengeId))
                {
                    ChallengeStandings.Add(currentSeasonChallengeId, new ChallengeStanding(_dataView.CurrentSeason.GetChallengeById(currentSeasonChallengeId)));
                }
            }
        }

        private void SetChallengeStanding(PlayerResult playerResult, Player foundPlayer)
        {
            var handicap = _dataView.GetPlayerHandicap(foundPlayer);
            var fastestLapWithHandicap = playerResult.FastestLap + handicap;
            var challengePlayerStanding = new ChallengePlayerStanding(foundPlayer, fastestLapWithHandicap);

            ChallengeStandings[playerResult.ChallengeId].SetChallengePlayerStanding(challengePlayerStanding);
        }

        public void UpdateUI()
        {
            CreateChallengeStandings();
        }
    }
}

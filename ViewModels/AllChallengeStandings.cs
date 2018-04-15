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
            foreach (PlayerResult playerResult in _dataView.PlayerResults)
            {
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
        }

        private void SetChallengeStanding(PlayerResult playerResult, Player foundPlayer)
        {
            var handicap = _dataView.GetPlayerHandicap(foundPlayer);
            var fastestLapWithHandicap = playerResult.FastestLap + handicap;
            var challengePlayerStanding = new ChallengePlayerStanding(foundPlayer, fastestLapWithHandicap);

            ChallengeStandings[playerResult.ChallengeId].AddChallengePlayerStanding(challengePlayerStanding);
        }
    }
}

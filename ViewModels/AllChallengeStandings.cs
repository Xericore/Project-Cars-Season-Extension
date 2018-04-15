using System.Collections.Generic;
using System.Linq;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class AllChallengeStandings
    {
        private DataView _dataView;
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
                    Challenge challenge = _dataView.CurrentSeason.GetChallengeById(playerResult.ChallengeId);

                    ChallengeStanding challengeStanding = new ChallengeStanding(challenge);

                    ChallengeStandings.Add(playerResult.ChallengeId, challengeStanding);
                }

                PlayerModel foundPlayer = _dataView.Players.FirstOrDefault(p => p.Id == playerResult.PlayerId);

                if (foundPlayer != null)
                {
                    var challengePlayerStanding = new ChallengePlayerStanding(foundPlayer, playerResult.FastestLap);

                    ChallengeStandings[playerResult.ChallengeId].AddChallengePlayerStanding(challengePlayerStanding);
                }
            }
        }
    }
}

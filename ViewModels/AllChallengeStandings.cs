using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class AllChallengeStandings
    {
        public ObservableCollection<ChallengeStanding> ChallengeStandings { get; set; } = new ObservableCollection<ChallengeStanding>();

        private readonly DataView _dataView;
        private readonly Dictionary<int, ChallengeStanding> _challengeStandings = new Dictionary<int, ChallengeStanding>();
        
        public AllChallengeStandings(DataView dataView)
        {
            _dataView = dataView;
            CreateChallengeStandings();
        }

        private void CreateChallengeStandings()
        {
            _challengeStandings.Clear();

            foreach (PlayerResult playerResult in _dataView.PlayerResults)
            {
                if (!_dataView.CurrentSeason.ContainsChallenge(playerResult.ChallengeId))
                    continue;

                if (!_challengeStandings.ContainsKey(playerResult.ChallengeId))
                {
                    Challenge challenge = _dataView.GetChallengeById(playerResult.ChallengeId);
                    ChallengeStanding challengeStanding = new ChallengeStanding(challenge);
                    _challengeStandings.Add(playerResult.ChallengeId, challengeStanding);
                }

                Player foundPlayer = _dataView.Players.FirstOrDefault(p => p.Id == playerResult.PlayerId);

                if (foundPlayer != null)
                {
                    SetChallengeStanding(playerResult, foundPlayer);
                }
            }

            AddChallengeStandingsWithoutPlayerResults();

            ChallengeStandings.Clear();
            foreach (ChallengeStanding challengeStanding in _challengeStandings.Values)
            {
                ChallengeStandings.Add(challengeStanding);
            }
        }

        private void AddChallengeStandingsWithoutPlayerResults()
        {
            if (_dataView.CurrentSeason == null)
                return;

            foreach (var currentSeasonChallengeId in _dataView.CurrentSeason.ChallengeIds)
            {
                if (!_challengeStandings.ContainsKey(currentSeasonChallengeId))
                {
                    _challengeStandings.Add(currentSeasonChallengeId, new ChallengeStanding(_dataView.CurrentSeason.GetChallengeById(currentSeasonChallengeId)));
                }
            }
        }

        private void SetChallengeStanding(PlayerResult playerResult, Player foundPlayer)
        {
            var handicap = _dataView.GetPlayerHandicap(foundPlayer);
            var fastestLapWithHandicap = playerResult.FastestLap + handicap;
            var challengePlayerStanding = new ChallengePlayerStanding(foundPlayer, fastestLapWithHandicap);

            _challengeStandings[playerResult.ChallengeId].SetChallengePlayerStanding(challengePlayerStanding);
        }

        public int GetPlayerPosition(ChallengeResult challengeResult, Player selectedPlayer)
        {
            var challengeStanding =
                ChallengeStandings.FirstOrDefault(
                    c => c.Challenge.ToString() == challengeResult.ToString());

            if (challengeStanding != null)
                return challengeStanding.GetPlayerPosition(selectedPlayer.Id);
            
            return -1;
        }

        public void UpdateDataAndUI()
        {
            CreateChallengeStandings();
        }
    }
}

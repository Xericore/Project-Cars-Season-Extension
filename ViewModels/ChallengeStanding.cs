using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Utils;
using ProjectCarsSeasonExtension.Views;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengeStanding : BaseModel
    {
        public Challenge Challenge { get; set; }
        public ChallengeView ChallengeView { get; set; }

        public SortedSet<ChallengePlayerStanding> ChallengePlayerStandings { get; set; } = new SortedSet<ChallengePlayerStanding>();

        public ChallengeStanding(Challenge challenge)
        {
            Challenge = challenge;

            ChallengeView = new ChallengeView(this);
        }

        public void SetChallengePlayerStanding(ChallengePlayerStanding challengePlayerStanding)
        {
            var foundChallengePlayerStanding =
                ChallengePlayerStandings.FirstOrDefault(cps => cps.Player.Id == challengePlayerStanding.Player.Id);

            if (foundChallengePlayerStanding != null)
            {
                ChallengePlayerStandings.Remove(foundChallengePlayerStanding);
            }

            ChallengePlayerStandings.Add(challengePlayerStanding);

            SetPositionAndTimeGapProperties();
            OnPropertyChanged(nameof(ChallengePlayerStandings));
        }

        private void SetPositionAndTimeGapProperties()
        {
            ChallengePlayerStanding previousChallengePlayerStanding = null;
            uint count = 1;
            foreach (var challengePlayerStanding in ChallengePlayerStandings)
            {
                if (previousChallengePlayerStanding != null)
                {
                    TimeSpan deltaToPreviousPlayer =
                        challengePlayerStanding.FastestLapWithHandicap - previousChallengePlayerStanding.FastestLapWithHandicap;

                    challengePlayerStanding.GapToPreviousPlayer = deltaToPreviousPlayer;
                }

                challengePlayerStanding.GapToLeader = challengePlayerStanding.FastestLapWithHandicap - ChallengePlayerStandings.First().FastestLapWithHandicap;

                challengePlayerStanding.Position = count;
                count++;
                previousChallengePlayerStanding = challengePlayerStanding;
            }
        }

        public int GetPlayerPoints(int playerId)
        {
            int position = 0;
            foreach (var challengePlayerStanding in ChallengePlayerStandings)
            {
                position++;
                if (challengePlayerStanding.Player.Id == playerId)
                    return PointsUtil.PositionToPoints(position);
            }

            return 0;
        }

        public int GetPlayerPosition(int playerId)
        {
            int position = 0;
            foreach (var challengePlayerStanding in ChallengePlayerStandings)
            {
                position++;
                if (challengePlayerStanding.Player.Id == playerId)
                    return position;
            }

            return -1;
        }
    }
}
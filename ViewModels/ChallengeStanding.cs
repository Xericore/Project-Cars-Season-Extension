using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengeStanding : BaseModel
    {
        public Challenge Challenge { get; set; }

        public SortedSet<ChallengePlayerStanding> ChallengePlayerStandings { get; set; } = new SortedSet<ChallengePlayerStanding>();

        public ChallengeStanding(Challenge challenge)
        {
            Challenge = challenge;
        }

        public void AddChallengePlayerStanding(ChallengePlayerStanding challengePlayerStanding)
        {
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
                        challengePlayerStanding.FastestLap - previousChallengePlayerStanding.FastestLap;

                    challengePlayerStanding.GapToPreviousPlayer = deltaToPreviousPlayer;
                }

                challengePlayerStanding.GapToLeader = challengePlayerStanding.FastestLap - ChallengePlayerStandings.First().FastestLap;

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
    }
}
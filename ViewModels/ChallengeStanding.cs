using System.Collections.Generic;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengeStanding
    {
        public Challenge Challenge { get; set; }
        public SortedSet<ChallengePlayerStanding> ChallengePlayerStandings { get; set; } = new SortedSet<ChallengePlayerStanding>();

        public ChallengeStanding(Challenge challenge)
        {
            Challenge = challenge;
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
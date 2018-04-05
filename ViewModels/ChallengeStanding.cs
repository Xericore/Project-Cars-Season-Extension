using System;
using System.Collections.Generic;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengeStanding
    {
        public Challenge Challenge { get; set; }
        public SortedList<TimeSpan, PlayerModel> SortedPlayers { get; set; } = new SortedList<TimeSpan, PlayerModel>();

        public ChallengeStanding(Challenge challenge)
        {
            Challenge = challenge;
        }

        public int GetPlayerPoints(int playerId)
        {
            int position = 0;
            foreach (KeyValuePair<TimeSpan, PlayerModel> sortedPlayer in SortedPlayers)
            {
                position++;
                if (sortedPlayer.Value.Id == playerId)
                    return PointsUtil.PositionToPoints(position);
            }

            return 0;
        }
    }
}
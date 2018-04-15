using System;

namespace ProjectCarsSeasonExtension.Models.Player
{
    public class PlayerHandicap
    {
        public int PlayerId { get; }
        public int SeasonId { get; }

        public TimeSpan Handicap { get; set; }

        public PlayerHandicap(int playerId, int seasonId, TimeSpan handicap)
        {
            PlayerId = playerId;
            SeasonId = seasonId;
            Handicap = handicap;
        }
    }
}

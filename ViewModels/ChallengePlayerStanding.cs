using System;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengePlayerStanding : IComparable
    {
        public uint Position { get; set; }
        public PlayerModel Player { get; }
        public TimeSpan FastestLapWithHandicap { get; }
        public TimeSpan GapToPreviousPlayer { get; set; }
        public TimeSpan GapToLeader { get; set; }

        public ChallengePlayerStanding(PlayerModel player, TimeSpan fastestLapWithHandicap)
        {
            Player = player;
            FastestLapWithHandicap = fastestLapWithHandicap;
        }

        public int CompareTo(object o)
        {
            ChallengePlayerStanding a = this;
            ChallengePlayerStanding b = (ChallengePlayerStanding)o;

            if(a.FastestLapWithHandicap < b.FastestLapWithHandicap)
                return -1;

            if (a.FastestLapWithHandicap == b.FastestLapWithHandicap)
                return 0;

            return 1;
        }
    }
}
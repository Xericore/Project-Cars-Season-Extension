﻿using System;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengePlayerStanding : IComparable
    {
        public uint Position { get; set; }
        public PlayerModel Player { get; }
        public TimeSpan FastestLap { get; }
        public TimeSpan GapToPreviousPlayer { get; set; }
        public TimeSpan GapToLeader { get; set; }

        public ChallengePlayerStanding(PlayerModel player, TimeSpan fastestLap)
        {
            Player = player;
            FastestLap = fastestLap;
        }

        public int CompareTo(object o)
        {
            ChallengePlayerStanding a = this;
            ChallengePlayerStanding b = (ChallengePlayerStanding)o;

            if(a.FastestLap < b.FastestLap)
                return -1;

            if (a.FastestLap == b.FastestLap)
                return 0;

            return 1;
        }
    }
}
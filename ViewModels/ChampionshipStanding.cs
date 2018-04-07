﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChampionshipStanding : IComparable
    {
        public PlayerModel Player { get; set; }

        public ObservableCollection<int> ChallengePoints { get; } = new ObservableCollection<int>();

        public int TotalPoints => ChallengePoints.Sum();

        public ChampionshipStanding(PlayerModel playerName)
        {
            Player = playerName;
        }

        public int CompareTo(object o)
        {
            ChampionshipStanding a = this;
            ChampionshipStanding b = (ChampionshipStanding)o;

            if (a.TotalPoints < b.TotalPoints)
                return 1;

            if (a.TotalPoints == b.TotalPoints)
                return 0;

            return -1;
        }
    }
}
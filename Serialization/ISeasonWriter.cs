﻿using System.Collections.Generic;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public interface ISeasonWriter
    {
        void SavePlayers(IEnumerable<Player> players);
        void SaveChallenges(IEnumerable<Challenge> challenges);
        void SaveSeasons(IEnumerable<Season> seasons);
        void SavePlayerResults(IEnumerable<PlayerResult> playerResults);
        void SaveHandicaps(IEnumerable<PlayerHandicap> handicaps);
    }
}

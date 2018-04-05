using System;
using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class ChallengeStanding
    {
        public Challenge Challenge { get; set; }
        public ObservableCollection<ChallengePlayerStanding> ChallengePlayerStandings { get; set; } = new ObservableCollection<ChallengePlayerStanding>();

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

    public class ChallengePlayerStanding
    {
        public PlayerModel Player { get; }
        public TimeSpan FastestLap { get; }

        public ChallengePlayerStanding(PlayerModel player, TimeSpan fastestLap)
        {
            Player = player;
            FastestLap = fastestLap;
        }
    }
}
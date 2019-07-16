using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public static class AllChallengeStandingsFactory
    {
        public static AllChallengeStandings CreateAllChallengeStandings(DataView dataView)
        {
            return new AllChallengeStandings(dataView);
        }
        public static AllChallengeStandings CreateAllRookieChallengeStandings(DataView dataView)
        {
            return new AllChallengeStandings(dataView, true);
        }
    }
}

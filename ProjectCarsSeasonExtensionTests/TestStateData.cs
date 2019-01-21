using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.ChallengeResultSender;

namespace ProjectCarsSeasonExtensionTests
{
    internal struct TestStateData : IProjectCarsStateData
    {
        public string CarName { get; }
        public string TrackLocation { get; }
        public string TrackVariant { get; }
        public float LastLapTime { get; }
        public bool LapInvalidated { get; }
        public GameState GameState { get; }
        public SessionState SessionState { get; }
        public RaceState RaceState { get; }

        public TestStateData(
            float lastLapTime = 0f, 
            bool lapInvalidated = false,
            GameState gameState = GameState.GameIngamePlaying, 
            SessionState sessionState = SessionState.SessionTimeAttack, 
            RaceState raceState = RaceState.RacestateRacing)
        {
            CarName = "TestStateDataCar";
            TrackLocation = "TestStateDataLocation";
            TrackVariant = "TestStateDataVariant";

            LastLapTime = lastLapTime;
            LapInvalidated = lapInvalidated;

            GameState = gameState;
            SessionState = sessionState;
            RaceState = raceState;
        }
    }
}

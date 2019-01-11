using pCarsAPI_Demo;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public struct ProjectCarsStateData
    {
        public string CarName { get; }
        public string TrackLocation { get; }
        public string TrackVariant { get; }
        public float LastLapTime { get; }
        public bool LapInvalidated { get; }
        public uint LapsInEvent { get; }
        public GameState GameState { get; }
        public SessionState SessionState { get; }
        public RaceState RaceState { get; }

        public ProjectCarsStateData(
            string carName, string trackLocation, string trackVariant, 
            float lastLapTime, bool lapInvalidated, uint lapsInEvent, 
            GameState gameState, SessionState sessionState, RaceState raceState)
        {
            CarName = carName;
            TrackLocation = trackLocation;
            TrackVariant = trackVariant;

            LastLapTime = lastLapTime;
            LapInvalidated = lapInvalidated;
            LapsInEvent = lapsInEvent;

            GameState = gameState;
            SessionState = sessionState;
            RaceState = raceState;
        }
    }
}

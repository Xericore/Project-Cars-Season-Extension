using pCarsAPI_Demo;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public struct ProjectCarsStateData : IProjectCarsStateData
    {
        public string CarName { get; }
        public string TrackLocation { get; }
        public string TrackVariant { get; }

        public float LastLapTime { get; }
        public bool LapInvalidated { get; }

        public GameState GameState { get; }
        public SessionState SessionState { get; }
        public RaceState RaceState { get; }

        public ProjectCarsStateData(
            string carName, string trackLocation, string trackVariant, 
            float lastLapTime, bool lapInvalidated, 
            GameState gameState, SessionState sessionState, RaceState raceState)
        {
            CarName = carName;
            TrackLocation = trackLocation;
            TrackVariant = trackVariant;

            LastLapTime = lastLapTime;
            LapInvalidated = lapInvalidated;

            GameState = gameState;
            SessionState = sessionState;
            RaceState = raceState;
        }

        public ProjectCarsStateData(pCarsDataClass projectCarsData)
        {
            CarName = projectCarsData.CarName;
            TrackLocation = projectCarsData.TrackLocation;
            TrackVariant = projectCarsData.TrackVariant;

            LastLapTime = projectCarsData.LastLapTime;
            LapInvalidated = projectCarsData.LapInvalidated;

            GameState = projectCarsData.GameState;
            SessionState = projectCarsData.SessionState;
            RaceState = projectCarsData.RaceState;
        }
    }
}

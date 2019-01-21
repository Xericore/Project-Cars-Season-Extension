using pCarsAPI_Demo;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public interface IProjectCarsStateData
    {
        string CarName { get; }
        string TrackLocation { get; }
        string TrackVariant { get; }

        float LastLapTime { get; }
        bool LapInvalidated { get; }

        GameState GameState { get; }
        SessionState SessionState { get; }
        RaceState RaceState { get; }
    }
}
